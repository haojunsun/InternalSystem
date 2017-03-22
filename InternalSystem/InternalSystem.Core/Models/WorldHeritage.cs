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
        /// 正题名
        /// </summary>
        public string TitleProper { get; set; }

        /// <summary>
        /// 其他题名
        /// </summary>
        public string OtherTitle { get; set; }
        /// <summary>
        /// 艺术门类 一级分类
        /// </summary>
        public string FirstLevelOfArtClassification { get; set; }
        /// <summary>
        /// 艺术门类 二级分类
        /// </summary>
        public string SecondLevelOfArtClassification { get; set; }
        /// <summary>
        /// 艺术门类 三级分类
        /// </summary>
        public string ThirdLevelOfArtClassification { get; set; }

        /// <summary>
        /// 民族属性 一级分类
        /// </summary>
        public string FirstLevelOfEthnicGroup { get; set; }
        /// <summary>
        /// 民族属性 二级分类
        /// </summary>
        public string SecondLevelOfEthnicGroup { get; set; }
        /// <summary>
        /// 民族属性 三级分类
        /// </summary>
        public string Subgroup { get; set; }

        /// <summary>
        /// 社会属性 一级分类
        /// </summary>
        public string FirstLevelOfSocialAttributes { get; set; }
        /// <summary>
        /// 社会属性 二级分类
        /// </summary>
        public string SecondLevelOfSocialAttributes { get; set; }

        /// <summary>
        /// 艺术形态 时代属性
        /// </summary>
        public string TimesProperty { get; set; }
        /// <summary>
        /// 艺术形态  组成要素 一级分类
        /// </summary>
        public string FirstLevelOfElements { get; set; }
        /// <summary>
        /// 艺术形态 组成要素 二级分类
        /// </summary>
        public string SecondLevelOfElements { get; set; }
        /// <summary>
        /// 艺术形态 组成要素 三级分类
        /// </summary>
        public string ThirdLevelOfElements { get; set; }

        /// <summary>
        /// 脚色行当 一级分类
        /// </summary>
        public string FirstLevelOfCharacter { get; set; }
        /// <summary>
        /// 脚色行当 二级分类
        /// </summary>
        public string SecondLevelOfCharacter { get; set; }
        /// <summary>
        /// 艺术形态 艺术流派
        /// </summary>
        public string ArtSchool { get; set; }
        /// <summary>
        /// 艺术形态 表演形式
        /// </summary>
        public string PerformingForm { get; set; }
        /// <summary>
        /// 艺术形态 结构体式
        /// </summary>
        public string Structure { get; set; }
        /// <summary>
        /// 曲牌板式 曲牌名
        /// </summary>
        public string SongPattern { get; set; }
        /// <summary>
        /// 曲牌板式 板式 
        /// </summary>
        public string BeatsPattern { get; set; }
        /// <summary>
        /// 律吕宫调 民间调式
        /// </summary>
        public string MusicModeName { get; set; }
        /// <summary>
        /// 律吕宫调 工尺谱系
        /// </summary>
        public string GongCheModeName { get; set; }
        /// <summary>
        /// 律吕宫调 简谱系统
        /// </summary>
        public string NumberedMusicalNotationModeName { get; set; }
        /// <summary>
        /// 律吕宫调 《九宫大成》系统
        /// </summary>
        public string JiuGongDaChengModeName { get; set; }


        /// <summary> 
        /// 资源属性 一级分类
        /// </summary>
        public string FirstLevelOfSubjectClassification { get; set; }
        /// <summary>
        /// 资源属性 二级分类
        /// </summary>
        public string SecondLevelOfResourcesClassification { get; set; }
        /// <summary>
        /// 资源属性 三级分类
        /// </summary>
        public string ThirdLevelOfResourcesClassification { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 描述 摘要
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// 描述 获奖 奖项名称
        /// </summary>
        public string NameOfAwards { get; set; }
        /// <summary>
        /// 描述 获奖 获奖年度或届次
        /// </summary>
        public string YearOrTimeOfAwards { get; set; }
        /// <summary>
        /// 描述 获奖 颁奖单位
        /// </summary>
        public string Awarder { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 相关人物/组织 人物/组织名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 责任方式
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string NationalityOfRelatedPeople { get; set; }
        /// <summary>
        /// 籍贯 
        /// </summary>
        public string BirthplaceOfRelatedPeople { get; set; }
        /// <summary>
        /// 性别 
        /// </summary>
        public string GenderOfRelatedPeople { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string AgeOfRelatedPeople { get; set; }
        /// <summary>
        /// 文化程度
        /// </summary>
        public string EducationLevelOfRelatedPeople { get; set; }
        /// <summary>
        /// 职业/职称
        /// </summary>
        public string VocationOfRelatedPeople { get; set; }

        /// <summary>
        /// 时间类型
        /// </summary>
        public string TemporalType { get; set; }
        /// <summary>
        /// 历史纪年 起始时间
        /// </summary>
        public string FromHistoricalCalendar { get; set; }
        /// <summary>
        /// 历史纪年 结束时间
        /// </summary>
        public string ToHistoricalCalendar { get; set; }
        /// <summary>
        /// 公元纪年 起始时间
        /// </summary>
        public string FromADCalendar { get; set; }
        /// <summary>
        /// 公元纪年 结束时间
        /// </summary>
        public string ToADCalendar { get; set; }
        /// <summary>
        /// 空间类型
        /// </summary>
        public string SpatialType { get; set; }
        /// <summary>
        /// 地域/地点名称 时地名
        /// </summary>
        public string HistoricalPlaceName { get; set; }
        /// <summary>
        /// 地域/地点名称 今地名
        /// </summary>
        public string PresentPlaceName { get; set; }
        /// <summary>
        /// 地域/地点名称 其他地名
        /// </summary>
        public string OtherPlaceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Town { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Village { get; set; }
        /// <summary>
        /// 场地
        /// </summary>
        public string Venues { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        public string LongitudeLatitude { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }


        /// <summary>
        /// 关联 包含
        /// </summary>
        public string HasPart { get; set; }
        /// <summary>
        /// 关联 包含于
        /// </summary>
        public string IsPartOf { get; set; }
        /// <summary>
        /// 关联 参照
        /// </summary>
        public string Refer { get; set; }
        /// <summary>
        /// 关联 有格式为
        /// </summary>
        public string HasFormat { get; set; }

        /// <summary>
        /// 来源 出处/源资料名称
        /// </summary>
        public string NameReferences { get; set; }
        /// <summary>
        /// 源资料创建者 责任方式 
        /// </summary>
        public string NameofReferencesCreator { get; set; }
        /// <summary>
        /// 国际标准书号/国际标准音像制品编
        /// </summary>
        public string RoleofReferencesCreator { get; set; }

        /// <summary>
        /// 国际标准书号/国际标准音像制品编
        /// </summary>
        public string ISBNISRC { get; set; }
        /// <summary>
        /// 源资料格式类型
        /// </summary>
        public string ReferencesFormat { get; set; }
        /// <summary>
        /// 
        /// 获取方式
        /// </summary>
        public string AcquisitionMethod { get; set; }
        /// <summary>
        ///
        /// 源资料提供者
        /// </summary>
        public string ProviderOfReferences { get; set; }

        /// <summary>
        /// 源资料典藏单位
        /// </summary>
        public string RepositoryName { get; set; }

        /// <summary>
        /// 出版 出版者
        /// </summary>

        public string Publisher { get; set; }
        /// <summary>
        /// 出版 出版时间
        /// </summary> 
        public string PublicationDate { get; set; }
        /// <summary>
        /// 出版 出版地
        /// </summary>
        public string PlaceOfPublication { get; set; }

        /// <summary>
        /// 数字化信息  数字化格式
        /// </summary>
        public string DigitalObjectFormat { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 分辨率（解析度）
        /// </summary>
        public string DigitalSpecification { get; set; }
        /// <summary>
        /// 音/视频数据码率
        /// </summary>
        public string AudioVideoBitRate { get; set; }
        /// <summary>
        /// 音频采样频率
        /// </summary>
        public string AudioSamplingFrequency { get; set; }
        /// <summary>
        /// 声道数
        /// </summary>
        public string NumberOfChannels { get; set; }
        /// <summary>
        /// 储存地址
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 显示级别
        /// </summary>
        public string DisplayType { get; set; }
        /// <summary>
        /// 备份光盘存放编号
        /// </summary>
        public string CDNumber { get; set; }
        /// <summary>
        /// 授权者
        /// </summary>
        public string Holder { get; set; }
        /// <summary>
        /// 授权起止时间
        /// </summary>
        public string RightsDate { get; set; }
        /// <summary>
        /// 授权条款
        /// </summary>
        public string Licens { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Note { get; set; }


        /*附表一 名称重复 添加 前缀*/
        public string ArtificialID { get; set; }
        public string RelatedPeople_Name { get; set; }
        public string RelatedPeople_Role { get; set; }
        public string RelatedPeople_NationalityOfRelatedPeople { get; set; }
        public string RelatedPeople_BirthplaceOfRelatedPeople { get; set; }
        public string RelatedPeople_GenderOfRelatedPeople { get; set; }
        public string RelatedPeople_AgeOfRelatedPeople { get; set; }
        public string RelatedPeople_EducationLevelOfRelatedPeople { get; set; }
        public string RelatedPeople_VocationOfRelatedPeople { get; set; }

        /*附表二 名称重复 添加 前缀*/

        public string CoverageTemporal_ArtificialID { get; set; }
        public string CoverageTemporal_TemporalType { get; set; }
        public string CoverageTemporal_FromHistoricalCalendar { get; set; }
        public string CoverageTemporal_ToHistoricalCalendar { get; set; }
        public string CoverageTemporal_FromADCalendar { get; set; }
        public string CoverageTemporal_ToADCalendar { get; set; }

        /*附表三 名称重复 添加 前缀*/

        public string CoverageSpatial_ArtificialID { get; set; }
        public string CoverageSpatial_SpatialType { get; set; }
        public string CoverageSpatial_HistoricalPlaceName { get; set; }
        public string CoverageSpatial_PresentPlaceName { get; set; }
        public string CoverageSpatial_OtherPlaceName { get; set; }
        public string CoverageSpatial_province { get; set; }
        public string CoverageSpatial_City { get; set; }
        public string CoverageSpatial_County { get; set; }
        public string CoverageSpatial_Town { get; set; }
        public string CoverageSpatial_Village { get; set; }
        public string CoverageSpatial_Venues { get; set; }
        public string CoverageSpatial_LongitudeLatitude { get; set; }

        /*附表四 名称重复 添加 前缀*/

        public string CreatorofReferences_ArtificialID { get; set; }
        public string CreatorofReferences_NameofCreator { get; set; }
        public string CreatorofReferences_RoleofReferencesCreator { get; set; }

        /// <summary>
        /// 著录者
        /// </summary>
        public string Recorder { get; set; }
        /// <summary>
        /// 著录时间
        /// </summary>
        public string RecordingTime { get; set; }

        /// <summary>
        /// 更新记录者
        /// </summary>
        public string Modifier { get; set; }
        /// <summary>
        /// 更新记录时间
        /// </summary>
        public string ModifiedDate { get; set; }
        /// <summary>
        /// 审核者
        /// </summary>
        public string MetadataAuditor { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string MetadataAuditoTime { get; set; }
        /// <summary>
        /// 管理机构
        /// </summary>
        public string MetadataManagement { get; set; }



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
        /// 创建的资源iseffect=0 未审核状态，审核后iseffect=1，审核拒绝iseffect=2，
        /// </summary>
        public int IsEffect { get; set; }


        public virtual Manager User { get; set; }

        /// <summary>
        /// 类型：视频 0 文字1 图片2
        /// </summary>
        public int HeritageType { get; set; }

        [MaxLength]
        public string Content { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow { get; set; }

        /// <summary>
        /// 发布者
        /// </summary>
        public virtual Manager Release { get; set; }

        public string ReleaseDateTime { get; set; }
    }
}
