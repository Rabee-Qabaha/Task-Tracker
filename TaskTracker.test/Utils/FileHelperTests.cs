using Moq;
using TaskTracker.Utils;
using TaskTracker.Utils.Interfaces;

namespace TaskTracker.test.Utils;

public class FileHelperTests
{
    
    private readonly string _testFilePath;
    public FileHelperTests()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    }
    
    [Fact]
    private async Task EnsureFileExists_ShouldThrowArgumentException_WhenFilePathIsNull()
    {
        //Arrange
        const string invalidFilePath = "";
        var mockFileSystem = new Mock<IFileSystem>();
        mockFileSystem.Setup(fs=> fs.FileExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        
        var fileHelper = new FileHelper(mockFileSystem.Object);
        
        //Act + Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => fileHelper.EnsureFileExistsAsync(invalidFilePath, nameof(invalidFilePath)));
        Assert.Equal("File path cannot be null or empty (Parameter 'filePath')", exception.Message);
    }
    
    [Fact]
    private async Task EnsureFileExists_ShouldNotOverwriteFile_WhenFilePathCorrect()
    {
        //Arrange: create an existing file with content 
        const string existingContent = "{\"name\": \"Task\"}";

        var mockFileSystem = new Mock<IFileSystem>();
        mockFileSystem.Setup(fs => fs.FileExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        mockFileSystem.Setup(fs => fs.ReadAllTextAsync(It.IsAny<string>())).ReturnsAsync(existingContent);
        mockFileSystem.Setup(fs => fs.WriteAllTextAsync(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
        
        var fileHelper = new FileHelper(mockFileSystem.Object);

        await fileHelper.WriteFileAsync(_testFilePath, existingContent);

        //Act 
        await fileHelper.EnsureFileExistsAsync(_testFilePath, nameof(_testFilePath));
        
        //Assert
        var actualContent = await fileHelper.ReadFileAsync(_testFilePath);
        Assert.Equal(existingContent, actualContent);
        mockFileSystem.Verify(fs => fs.WriteAllTextAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
    
    [Fact]
    private async Task EnsureFileExists_ShouldCreateFile_WhenFilePathCorrect()
    {
        //Arrange
        var mockFileSystem = new Mock<IFileSystem>();
        mockFileSystem.Setup(fs => fs.FileExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        var fileHelper = new FileHelper(mockFileSystem.Object);
            
        //Act 
         await fileHelper.EnsureFileExistsAsync(_testFilePath, nameof(_testFilePath));
        
        //Assert
        mockFileSystem.Verify(fs => fs.WriteAllTextAsync(_testFilePath, nameof(_testFilePath)), Times.Never);
    }
    
    [Theory]
    [InlineData(typeof(UnauthorizedAccessException))]
    [InlineData(typeof(IOException))]
    public async Task EnsureFileExists_ThrowsCorrectException_WhenAccessDenied(Type exceptionType)
    {
        //Arrange
        const string unauthorizedPath = "/restricted/testfile.json";
        
        var mockFileSystem = new Mock<IFileSystem>();
        mockFileSystem.Setup(fs => fs.FileExistsAsync(It.IsAny<string>()))
            .ThrowsAsync((Exception)Activator.CreateInstance(exceptionType)!);
        
        var fileHelper = new FileHelper(mockFileSystem.Object);
        
        //Act + Assert
        await Assert.ThrowsAsync(exceptionType, ()=> fileHelper.EnsureFileExistsAsync(unauthorizedPath));
    }
    
    [Fact]
    private async Task GetFilePath_ShouldReturnFilePath_WhenNoError()
    {
        //Arrange
        const string fileName = "testfile.json";
        const string currentDirectory = "/some/path";
        
        var mockFileSystem = new Mock<IFileSystem>();
        mockFileSystem.Setup(fs => fs.CombinePathAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync($"{currentDirectory}/{fileName}");
        var fileHelper = new FileHelper(mockFileSystem.Object);

        //Act
        var filePath = await fileHelper.GetFilePath(fileName);
        
        // Assert
        Assert.Equal("/some/path/testfile.json",filePath);
    }
    
    [Fact]
    private async Task GetFilePath_ShouldThrowUnauthorizedAccessException_WhenAccessDenied()
    {
        //Arrange
        const string fileName = "testfile.json";
        const string currentDirectory = "/some/path";
        
        var mockFileSystem = new Mock<IFileSystem>();
        mockFileSystem.Setup(fs => fs.CombinePathAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new UnauthorizedAccessException("Permission denied"));
        var fileHelper = new FileHelper(mockFileSystem.Object);
        
        //Act + Assert
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => fileHelper.GetFilePath(fileName));
        Assert.Equal("Permission denied", exception.Message);
    }

    [Fact]
    private async Task ReadFileAsync_ShouldThrowArgumentException_WhenFilePathIsNull()
    {
        //Arrange
        const string filePath = "";
        
        var mockFileSystem = new Mock<IFileSystem>();
        mockFileSystem.Setup(fs => fs.ReadAllTextAsync(It.IsAny<string>()))
            .ThrowsAsync(new ArgumentException("File path cannot be null or empty", nameof(filePath)));
                
        var fileHelper = new FileHelper(mockFileSystem.Object);
        
        //Act + Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => fileHelper.ReadFileAsync(filePath));
        Assert.Equal("File path cannot be null or empty (Parameter 'filePath')", exception.Message);
    }
    
    [Fact]
    private async Task ReadFileAsync_ShouldThrowFileNotFoundException_WhenFileNotExists()
    {
        //Arrange
        const string filePath = "testfile.json";
        
        var mockFileSystem = new Mock<IFileSystem>();
        mockFileSystem.Setup(fs => fs.FileExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
                
        var fileHelper = new FileHelper(mockFileSystem.Object);
        
        //Act + Assert
        var exception = await Assert.ThrowsAsync<FileNotFoundException>(() => fileHelper.ReadFileAsync(filePath));
        Assert.Equal("File not found", exception.Message);
    }

    [Fact]
    private async Task WriteFileAsync_ShouldThrowArgumentException_WhenFilePathIsNull()
    {
        //Arrange
        const string filePath = "";
        var mockFilesystem = new Mock<IFileSystem>();
        
        mockFilesystem.Setup(fs=> fs.WriteAllTextAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new ArgumentException("File path cannot be null or empty", nameof(filePath)));
        var fileHelper = new FileHelper(mockFilesystem.Object);
        
        //Act + Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(()=> fileHelper.WriteFileAsync(filePath, ""));
        Assert.Equal("File path cannot be null or empty (Parameter 'filePath')", exception.Message);
    }
    
    [Fact]
    private async Task WriteFileAsync_ShouldWriteContent_WhenFilePathIsCorrect()
    {
        // Arrange
        const string filePath = "/some/path/file.txt";
        var writeContent = string.Empty;
        var mockFileSystem = new Mock<IFileSystem>();
        
        mockFileSystem.Setup(fs => fs.WriteAllTextAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Callback<string, string>((file, content) => writeContent = content)
            .Returns(Task.CompletedTask);

        var fileHelper = new FileHelper(mockFileSystem.Object);
        const string fileContent = "{\"name\": \"Task\"}";

        // Act
        await fileHelper.WriteFileAsync(filePath, fileContent);

        // Assert
        Assert.Equal(fileContent, writeContent);
    }
}