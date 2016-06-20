using System;
using System.ComponentModel.DataAnnotations;

namespace InternalSystem.Core.Models
{
    public class WorldHeritage
    {
        public int WorldHeritageId { get; set; }

        /// <summary>
        /// 系统编码
        /// </summary>
        public string InventoryId { get; set; }

        /// <summary>
        /// 人工编码
        /// </summary>
        public string ArtificialId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string TitleProper { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 项目类别一级
        /// </summary>
        public string FirstLevel { get; set; }

        /// <summary>
        /// 项目类别二级 
        /// </summary>
        public string SecondLevel { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 子项序号
        /// </summary>
        public string SubItemNumber { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 项目申请时间
        /// </summary>
        public string ApplicationTime { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 民族支系
        /// </summary>
        public string NationalBranch { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birth { get; set; }

        /// <summary>
        /// 文化程度
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// 传承人
        /// </summary>
        public string Successor { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// 直辖市
        /// </summary>
        public string Municipalities { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 县级市
        /// </summary>
        public string CountyLevelCity { get; set; }

        /// <summary>
        /// 乡镇
        /// </summary>
        public string Township { get; set; }

        /// <summary>
        /// 村落
        /// </summary>
        public string Village { get; set; }

        /// <summary>
        /// 申报地区
        /// </summary>
        public string DeclarationArea { get; set; }

        /// <summary>
        /// 保护单位
        /// </summary>
        public string ProtectionUnit { get; set; }

        /// <summary>
        /// 生态区项目
        /// </summary>
        public string EcologicalZoneProject { get; set; }

        /// <summary>
        /// 生产性保护基地
        /// </summary>
        public string ProductiveProtectionBase { get; set; }

        /// <summary>
        /// 省区代码
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string PostTime { get; set; }

        /// <summary>
        /// 去世
        /// </summary>
        public string Death { get; set; }

        /// <summary>
        /// 通过
        /// </summary>
        public string Adopt { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建者责任方式
        /// </summary>
        public string ResponsibilityCreator { get; set; }

        /// <summary>
        /// 资料格式类型
        /// </summary>
        public string DataFormat { get; set; }

        /// <summary>
        /// 资料提供者
        /// </summary>
        public string DataProvider { get; set; }

        /// <summary>
        /// 资料典藏单位
        /// </summary>
        public string CollectionUnit { get; set; }

        /// <summary>
        /// 数字化格式
        /// </summary>
        public string DigitalFormat { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 分辨率
        /// </summary>
        public string ResolvingPower { get; set; }

        /// <summary>
        /// 视频码率
        /// </summary>
        public string KBps { get; set; }

        /// <summary>
        /// 采样频率
        /// </summary>
        public string SamplingFrequency { get; set; }

        /// <summary>
        /// 声道数
        /// </summary>
        public string Channels { get; set; }

        /// <summary>
        /// 存储地
        /// </summary>
        public string StoragePlace { get; set; }

        /// <summary>
        /// 显示级别
        /// </summary>
        public string DisplayLevel { get; set; }

        /// <summary>
        /// 著录者
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 著录时间
        /// </summary>
        public string DescriptionTime { get; set; }

        /// <summary>
        /// 审核者
        /// </summary>
        public string Audit { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public string AuditTime { get; set; }

        /// <summary>
        /// 管理机构
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// URL地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 文件名 上传多媒体 文件夹名称 路径的组成部分 /FileName/ArtificialId +"."+ DigitalFormat
        /// </summary>
        public string FileName { get; set; }


        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// 0失效 1有效
        /// </summary>
        public int IsEffect { get; set; }
    }
}
