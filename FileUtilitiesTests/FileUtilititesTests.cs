using Xunit;

namespace FileUtilitiesTests;

public class FileUtilititesTests
{
    [Theory]
    [InlineData("d:\\root", "d:\\before\\final.txt", "d:\\root\\final.txt")]
    [InlineData("d:\\root", "d:\\before\\subdir\\final.txt", "d:\\root\\subdir\\final.txt")]
    public void should_build_new_destination(string targetDir, string filePath, string expectedFilePath)
    {
    }
}