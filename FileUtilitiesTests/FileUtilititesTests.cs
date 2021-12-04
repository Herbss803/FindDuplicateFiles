using FileUtilities;
using FluentAssertions;
using Xunit;

namespace FileUtilitiesTests;

public class FileUtilititesTests
{
    [Theory]
    [InlineData("d:\root", "d:\before\final.txt", "d:\root\final.txt")]
    public void should_build_new_destination(string targetDir, string filePath, string expectedFilePath)
    {
        FileTools.getTargetPath(targetDir, filePath).Should().Be(expectedFilePath);
    }
}