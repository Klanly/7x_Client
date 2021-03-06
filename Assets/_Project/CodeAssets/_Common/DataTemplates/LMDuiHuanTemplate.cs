﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class LMDuiHuanTemplate : XmlLoadManager {

	//      id:唯一ID
	//		itemType：	道具类型
	//					0普通道具;		读ItemTemp表
	//		   		 	2装备;			读ZhuangBei表
	//					3当铺材料;		读ItemTemp表
	//					4秘宝;			读MiBao表
	//					5秘宝碎片;		读MibaoSuiPian表
	//					6装备进阶材料;	读ItemTemp表
	//					7武将;			读HeroGrow表
	//					8精魄;			读JingPo表
	//					9强化材料		读ItemTemp表
	//		itemId：		道具ID
	//		itemNum：	道具数量
	//		needNum：	兑换所需威望数
	//		weight：	    随机时出现该物品的概率权值
	//		site：		出现的位置
	
	public int id;
	
	public int itemType;
	
	public int itemId;
	
	public int itemNum;
	
	public int needNum;
	
	public int weight;
	
	public int site;

	public int needLv;

	public int max;

	public int ifRecomanded;
	
	public static List<LMDuiHuanTemplate> templates = new List<LMDuiHuanTemplate>();
	
	public static void LoadTemplates( EventDelegate.Callback p_callback = null ){
		UnLoadManager.DownLoad(PathManager.GetUrl(m_LoadPath + "LMGongXianDuihuan.xml"), CurLoad, UtilityTool.GetEventDelegateList( p_callback ), false );
	}
	
	public static void CurLoad(ref WWW www, string path, Object obj){
		{
			templates.Clear();
		}
		
		XmlReader t_reader = null;
		
		if( obj != null ){
			TextAsset t_text_asset = obj as TextAsset;
			
			t_reader = XmlReader.Create( new StringReader( t_text_asset.text ) );
			
			//			Debug.Log( "Text: " + t_text_asset.text );
		}
		else{
			t_reader = XmlReader.Create( new StringReader( www.text ) );
		}
		
		bool t_has_items = true;
		
		do{
			t_has_items = t_reader.ReadToFollowing( "LMGongXianDuihuan" );
			
			if( !t_has_items ){
				break;
			}
			
			LMDuiHuanTemplate t_template = new LMDuiHuanTemplate();
			
			{
				t_reader.MoveToNextAttribute();
				t_template.id = int.Parse( t_reader.Value );
				
				t_reader.MoveToNextAttribute();
				t_template.itemType = int.Parse( t_reader.Value );
				
				t_reader.MoveToNextAttribute();
				t_template.itemId = int.Parse( t_reader.Value );
				
				t_reader.MoveToNextAttribute();
				t_template.itemNum = int.Parse( t_reader.Value );
				
				t_reader.MoveToNextAttribute();
				t_template.needNum = int.Parse( t_reader.Value );
				
				t_reader.MoveToNextAttribute();
				t_template.weight = int.Parse( t_reader.Value );
				
				t_reader.MoveToNextAttribute();
				t_template.site = int.Parse( t_reader.Value );

				t_reader.MoveToNextAttribute();
				t_template.needLv = int.Parse( t_reader.Value );

				t_reader.MoveToNextAttribute();
				t_template.max = int.Parse( t_reader.Value );

				t_reader.MoveToNextAttribute();
				t_template.ifRecomanded = int.Parse( t_reader.Value );
			}
			
			//			t_template.Log();
			
			templates.Add( t_template );
		}
		while( t_has_items );
	}
	
	public static LMDuiHuanTemplate getLMDuiHuanTemplateById(int id)
	{
		foreach(LMDuiHuanTemplate template in templates)
		{
			if(template.id == id)
			{
				return template;
			}
		}
		
		Debug.LogError("XML ERROR: Can't get LMDuiHuanTemplate with id " + id);
		
		return null;
	}

	public static LMDuiHuanTemplate getLMDuiHuanTemplateByItemId(int id)
	{
		foreach(LMDuiHuanTemplate template in templates)
		{
			if(template.itemId == id)
			{
				return template;
			}
		}
		
		Debug.LogError("XML ERROR: Can't get LMDuiHuanTemplate with id " + id);
		
		return null;
	}
}
