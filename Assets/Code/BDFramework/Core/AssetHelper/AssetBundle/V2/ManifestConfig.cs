﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace BDFramework.ResourceMgr.V2
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class ManifestConfig
    {
        /// <summary>
        /// 资源Map
        /// </summary>
        public Dictionary<string, ManifestItem>
            ManifestMap { get; private set; } //= new Dictionary<string, ManifestItem>();

        /// <summary>
        /// json结构
        /// </summary>
        /// <param name="content"></param>
        public ManifestConfig(string content)
        {
            ManifestMap = JsonMapper.ToObject<Dictionary<string, ManifestItem>>(content);
        }

        public ManifestConfig()
        {
            ManifestMap = new Dictionary<string, ManifestItem>();
        }

        /// <summary>
        /// 获取单个依赖
        /// </summary>
        /// <param name="menifestName"></param>
        /// <returns>这个list外部不要修改</returns>
        public List<string> GetDependenciesByName(string name)
        {
            ManifestItem item = null;
            if (this.ManifestMap.TryGetValue(name, out item))
            {
                var list = new List<string>(item.Depend);
                return list;
            }

            BDebug.LogError("【config】不存在资源:" + name);
            return null;
        }


        /// <summary>
        /// 获取单个menifestItem
        /// </summary>
        /// <param name="manifestName"></param>
        /// <returns></returns>
        public ManifestItem GetManifest(string manifestName)
        {
            if (!string.IsNullOrEmpty(manifestName))
            {
                ManifestItem item = null;
                this.ManifestMap.TryGetValue(manifestName, out item);
                return item;
            }

            return null;
        }
    }
}