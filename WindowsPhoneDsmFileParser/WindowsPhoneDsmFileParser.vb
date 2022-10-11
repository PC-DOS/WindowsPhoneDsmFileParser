Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
''' <summary>
''' 用于解析 Windows Phone CAB 包的工具。
''' </summary>
''' <remarks></remarks>
Public Class WindowsPhoneDsmFileParser
    ''' <summary>
    ''' 描述 Windows Phone CAB 包的文件列表。
    ''' </summary>
    ''' <remarks></remarks>
    <XmlRoot(ElementName:="Files", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Class PackageFileList
        ''' <summary>
        ''' Windows Phone CAB 包的文件列表。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="FileEntry", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public FileList As New List(Of PackageFile)
    End Class
    ''' <summary>
    ''' 描述 Windows Phone CAB 包中单个文件的信息。
    ''' </summary>
    ''' <remarks></remarks>
    <XmlRoot(ElementName:="FileEntry", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Class PackageFile
        ''' <summary>
        ''' 文件的类型。
        ''' </summary>
        ''' <remarks>常见的类型包括: Manifest、Catalog、Registry、RegistryMultiStringAppend、Regular。</remarks>
        <XmlElement(ElementName:="FileType", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public FileType As String
        ''' <summary>
        ''' CAB 包被套用后该文件在系统中的路径。为相对于所在分区根目录的路径。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="DevicePath", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public DevicePath As String
        ''' <summary>
        ''' 该文件在 CAB 包中的路径。为相对于 CAB 包的根的路径。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="CabPath", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public CabPath As String
        ''' <summary>
        ''' 该文件的属性集合。
        ''' </summary>
        ''' <remarks>使用半角空格分隔多个属性。</remarks>
        <XmlElement(ElementName:="Attributes", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Attributes As String
        ''' <summary>
        ''' 该文件展开后的尺寸。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="FileSize", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public FileSize As String
        ''' <summary>
        ''' 该文件被压缩后的尺寸。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="CompressedFileSize", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public CompressedFileSize As String
        ''' <summary>
        ''' 暂存的文件尺寸。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="StagedFileSize", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public StagedFileSize As String
    End Class
    ''' <summary>
    ''' 描述 Windows Phone CAB 包的版本。
    ''' </summary>
    ''' <remarks></remarks>
    <XmlRoot(ElementName:="Version", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Class PackageVersion
        ''' <summary>
        ''' 主版本号。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlAttribute(AttributeName:="Major")> Public Major As String
        ''' <summary>
        ''' 次版本号。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlAttribute(AttributeName:="Minor")> Public Minor As String
        ''' <summary>
        ''' QFE 编号。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlAttribute(AttributeName:="QFE")> Public QFE As String
        ''' <summary>
        ''' 构建编号。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlAttribute(AttributeName:="Build")> Public Build As String
        ''' <summary>
        ''' 将版本号转换为字符串。
        ''' </summary>
        ''' <returns>版本号字符串，顺序为Majo.Minor.QFE.Build。</returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return Major.ToString & "." & Minor.ToString & "." & QFE.ToString & "." & Build.ToString
        End Function
        ''' <summary>
        ''' 产生一个新的 PackageVersion 实例，所有的值均为 0。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Major = "0"
            Minor = "0"
            QFE = "0"
            Build = "0"
        End Sub
        ''' <summary>
        ''' 从提供的信息产生一个新的 PackageVersion 实例。
        ''' </summary>
        ''' <param name="MajorVersion">主版本号，默认值为 0。</param>
        ''' <param name="MinorVersion">次版本号，默认值为 0。</param>
        ''' <param name="QFELevel">QFE编号，默认值为 0。</param>
        ''' <param name="BuildNumber">构建编号，默认值为 0。</param>
        ''' <remarks></remarks>
        Public Sub New(Optional MajorVersion As String = "0", Optional MinorVersion As String = "0", Optional QFELevel As String = "0", Optional BuildNumber As String = "0")
            Major = MajorVersion
            Minor = MinorVersion
            QFE = QFELevel
            Build = BuildNumber
        End Sub
    End Class
    ''' <summary>
    ''' 描述 Windows Phone CAB 包的识别信息。
    ''' </summary>
    ''' <remarks></remarks>
    <XmlRoot(ElementName:="Identity", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Class PackageIdentity
        ''' <summary>
        ''' 包所隶属的所有者。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="Owner", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Owner As String
        ''' <summary>
        ''' 包所提供的组件名称。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="Component", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Component As String
        ''' <summary>
        ''' 包所提供的子组件名称。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="SubComponent", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public SubComponent As String
        ''' <summary>
        ''' 包的版本号。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="Version", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Version As PackageVersion
        ''' <summary>
        ''' 返回包名字符串。
        ''' </summary>
        ''' <returns>包名字符串，顺序为Owner.Component.SubComponent.Majo.Minor.QFE.Build</returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return Owner & "." & Component & "." & SubComponent & "." & Version.ToString
        End Function
    End Class
    <XmlRoot(ElementName:="Package", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Class WindowsPhonePackageInformation
        ''' <summary>
        ''' XML 命名空间。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlAttribute(AttributeName:="xmlns")> Public XmlNS As String
        ''' <summary>
        ''' 指示包的识别信息。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="Identity", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Identity As PackageIdentity
        ''' <summary>
        ''' 指示包的发行类型。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="ReleaseType", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public ReleaseType As String
        ''' <summary>
        ''' 指示包的所有者类型。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="OwnerType", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public OwnerType As String
        ''' <summary>
        ''' 指示包的构造类型。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="BuildType", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public BuildType As String
        ''' <summary>
        ''' 指示包的目标处理器架构。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="CpuType", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public CpuType As String
        ''' <summary>
        ''' 指示包的目标平台。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="Platform", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Platform As String
        ''' <summary>
        ''' 指示包的目标分区。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="Partition", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Partition As String
        ''' <summary>
        ''' 指示包是否可被移除。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="IsRemoval", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public IsRemoval As String
        ''' <summary>
        ''' 指示包所包含的文件列表。
        ''' </summary>
        ''' <remarks></remarks>
        <XmlElement(ElementName:="Files", Namespace:="http://schemas.microsoft.com/embedded/2004/10/ImageUpdate")> Public Files As New PackageFileList
    End Class
    ''' <summary>
    ''' 一个 Windows Phone 包的信息。
    ''' </summary>
    ''' <remarks></remarks>
    Public _WindowsPhonePackage As New WindowsPhonePackageInformation
    ''' <summary>
    ''' 返回一个 Windows Phone 包的信息。
    ''' </summary>
    ''' <value>一个 Windows Phone 包的信息</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WindowsPhonePackage As WindowsPhonePackageInformation
        Get
            Return _WindowsPhonePackage
        End Get
    End Property
    ''' <summary>
    ''' 建立一个空的 Windows Phone 包对象。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _WindowsPhonePackage = New WindowsPhonePackageInformation
    End Sub
    ''' <summary>
    ''' 从 Windows Phone 包描述文件建立一个 Windows Phone 包对象。
    ''' </summary>
    ''' <param name="DsmFilePath">Windows Phone 包描述文件的路径。</param>
    ''' <remarks></remarks>
    Public Sub New(DsmFilePath As String)
        Dim Serializer As New XmlSerializer(GetType(WindowsPhonePackageInformation))
        Dim XmlFileStream As New FileStream(DsmFilePath, FileMode.OpenOrCreate)
        _WindowsPhonePackage = Serializer.Deserialize(XmlFileStream)
    End Sub
    ''' <summary>
    ''' 从 Windows Phone 包描述文件建立一个 Windows Phone 包对象。
    ''' </summary>
    ''' <param name="DsmFilePath">Windows Phone 包描述文件的路径。</param>
    ''' <remarks></remarks>
    Public Sub LoadPackage(DsmFilePath As String)
        Dim Serializer As New XmlSerializer(GetType(WindowsPhonePackageInformation))
        Dim XmlFileStream As New FileStream(DsmFilePath, FileMode.OpenOrCreate)
        _WindowsPhonePackage = Serializer.Deserialize(XmlFileStream)
        XmlFileStream.Close()
    End Sub
    ''' <summary>
    ''' 将当前 Windows Phone 包文件写入到一个 XML 文件中。
    ''' </summary>
    ''' <param name="OutputFilePath">要写入的 XML 文件的路径。</param>
    ''' <remarks></remarks>
    Public Sub SerializePackageToFile(OutputFilePath As String)
        Dim Serializer As New XmlSerializer(GetType(WindowsPhonePackageInformation))
        Dim XmlFileStream As New FileStream(OutputFilePath, FileMode.Create)
        Serializer.Serialize(XmlFileStream, _WindowsPhonePackage)
        XmlFileStream.Flush()
        XmlFileStream.Close()
    End Sub
End Class

