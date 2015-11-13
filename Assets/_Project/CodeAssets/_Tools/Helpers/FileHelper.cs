﻿
using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using qxmobile.protobuf;

public class FileHelper {

	#region Directory

	public static void DirectoryDelete( string p_full_path, bool p_recursive ){
		DirectoryInfo t_dir = new DirectoryInfo( p_full_path );

		if ( !t_dir.Exists ) {
//			Debug.LogError( "Delete Target not exist: " + p_full_path );

			return;
		}

		Directory.Delete ( p_full_path, p_recursive );
	}

	public static void DirectoryMove( string p_src, string p_dest ){
		Directory.Move( p_src, p_dest );
	}
	
	public static void DirectoryCopy( string p_src, string p_dest ){
		DirectoryInfo t_src_dir = new DirectoryInfo( p_src );
		
		DirectoryInfo[] t_src_dirs = t_src_dir.GetDirectories();
		
		if( !t_src_dir.Exists ){
			Debug.LogError( "Not Exist: " + p_src );
			
			return;
		}
		
		if( !Directory.Exists( p_dest ) ){
			Directory.CreateDirectory( p_dest );
		}
		
		FileInfo[] t_files = t_src_dir.GetFiles();
		
		foreach( FileInfo t_file in t_files ){
			string t_path = Path.Combine( p_dest, t_file.Name );
			
			t_file.CopyTo( t_path, true );
		}
		
		foreach( DirectoryInfo t_dir in t_src_dirs ){
			string t_path = Path.Combine( p_dest, t_dir.Name );
			
			DirectoryCopy( t_dir.FullName, t_path );
		}
	}

	#endregion



	#region File Ops

	public static void FileCopy( string p_src, string p_dest ){
		if ( File.Exists ( p_dest ) ) {
			File.Delete ( p_dest );
		}

		File.Copy( p_src, p_dest );
	}

	#endregion



	#region Stream
	
	/** Params:
    * p_file_name: Local_File.bin
    */
	public static System.IO.FileStream GetPersistentFileStream(string p_file_name)
	{
		string t_local_file_name = PathHelper.GetPersistentFilePath(p_file_name);
		
		System.IO.FileStream t_stream = new System.IO.FileStream(t_local_file_name,
		                                                         System.IO.FileMode.OpenOrCreate);
		
		#if UNITY_IPHONE
		UnityEngine.iOS.Device.SetNoBackupFlag( t_local_file_name );
		#endif
		
		return t_stream;
	}
	
	/// Params:
	/// p_file_name: Local_File.bin
	public static void DeletePersistentFileStream(string p_file_name)
	{
		string t_local_file_name = PathHelper.GetPersistentFilePath ( p_file_name );
		
		if (System.IO.File.Exists( t_local_file_name ) ) {
			System.IO.File.Delete( t_local_file_name );
		}
	}
	
	public static string ReadString(System.IO.FileStream p_stream)
	{
		byte[] t_bytes = new byte[p_stream.Length];
		
		p_stream.Read(t_bytes, 0, (int)p_stream.Length);
		
		string t_str = Encoding.UTF8.GetString(t_bytes);
		
		return t_str;
	}
	
	public static void WriteString(System.IO.FileStream p_stream, string p_data)
	{
		byte[] t_bytes = Encoding.UTF8.GetBytes(p_data);
		
		p_stream.Write(t_bytes, 0, t_bytes.Length);
	}
	
	/// Params:
	/// p_path: Application.dataPath + "/Resources/_Data/Config/Test/action.txt"
	public static void OutputFile( string p_path, string p_text ){
		FileStream t_file_stream = null;
		
		if (File.Exists(p_path))
		{
			t_file_stream = new FileStream(p_path, FileMode.Truncate);
		}
		else
		{
			t_file_stream = new FileStream(p_path, FileMode.Create);
		}
		
		StreamWriter t_stream_writer = new StreamWriter(
			t_file_stream,
			Encoding.Default);
		
		t_stream_writer.Write(p_text);
		
		t_stream_writer.Close();
		
		t_file_stream.Close();
	}
	
	#endregion


		
	#region File Log
	
	private const string LOG_FILE_NAME = "Log";
	
	public static void DeleteLogFile(){
		//		Debug.Log ( "DeleteLogFile( " + GetPersistentFilePath( LOG_FILE_NAME ) + " )" );
		
		DeletePersistentFileStream ( LOG_FILE_NAME );
	}
	
	public static void LogFile( string p_log, string p_stack, LogType p_type ){
		string t_log_string = p_type + ": " + p_log + 
			"\n" + p_stack + 
				"\n";
		
		//		Debug.Log ( "LogFile( " + t_log_string + " )" );
		
		System.IO.FileStream t_stream = FileHelper.GetPersistentFileStream( LOG_FILE_NAME );
		
		t_stream.Position = t_stream.Length;
		
		FileHelper.WriteString( t_stream, t_log_string );
		
		t_stream.Close();
	}
	
	#endregion
}
