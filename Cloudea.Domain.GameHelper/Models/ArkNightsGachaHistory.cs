﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.GameHelper.Models.ArkNights;

/// <summary>
/// 寻访记录信息 - 全部
/// </summary>
public class GachaHistory
{
    public List[] list { get; set; } = new List[0];
}

/// <summary>
/// 寻访记录信息 - 单页
/// </summary>
public class GachaHistoryPage
{
    /// <summary>
    /// 
    /// </summary>
    public int code { get; set; } = 100;
    /// <summary>
    /// 寻访记录相关信息
    /// </summary>
    public Data data { get; set; }
    /// <summary>
    /// 提示信息
    /// </summary>
    public string msg { get; set; }
}

public class Data
{
    /// <summary>
    /// 寻访记录
    /// </summary>
    public List[] list { get; set; } = new List[0];
    /// <summary>
    /// 页码
    /// </summary>
    public Pagination pagination { get; set; }
}

public class Pagination
{
    /// <summary>
    /// 当前页码
    /// </summary>
    public int current { get; set; }
    /// <summary>
    /// 总页码
    /// </summary>
    public int total { get; set; }
}

/// <summary>
/// 寻访记录 - 单条
/// </summary>
public class List
{
    /// <summary>
    /// Unix时间戳 - 秒
    /// </summary>
    public int ts { get; set; }
    /// <summary>
    /// 卡池名称
    /// </summary>
    public string pool { get; set; }
    /// <summary>
    /// 角色列表 - 单抽/十连
    /// </summary>
    public Char[] chars { get; set; }
}

/// <summary>
/// 角色信息
/// </summary>
public class Char
{
    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 稀有度
    /// </summary>
    public int rarity { get; set; }
    /// <summary>
    /// 首次获得
    /// </summary>
    public bool isNew { get; set; }
}
