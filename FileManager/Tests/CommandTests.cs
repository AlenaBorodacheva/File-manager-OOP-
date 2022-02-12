using FileManager;
using FileManager.Commands;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private string userDir = "Мои_файлы";
        private string currentDir = "C:\\";
        private string fromTo = "";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CopyTest()
        {
            var copy = new Copy(userDir, currentDir);
            var result = copy.Execute();

            Assert.Equals(result,Result.Ok);
        }

        [Test]
        public void CreateDirTest()
        {
            var createDir = new CreateDir(userDir, currentDir);
            var result = createDir.Execute();

            Assert.Equals(result, Result.Ok);
        }

        [Test]
        public void CreateFileTest()
        {
            var createFile = new CreateFile(userDir, currentDir);
            var result = createFile.Execute();

            Assert.Equals(result, Result.Ok);
        }

        [Test]
        public void DeleteTest()
        {
            var delete = new Delete(userDir, currentDir);
            var result = delete.Execute();

            Assert.Equals(result, Result.Ok);
        }

        [Test]
        public void FindTest()
        {
            var find = new Find(userDir, currentDir);
            var result = find.Execute();

            Assert.Equals(result, Result.Ok);
        }

        [Test]
        public void InfoTest()
        {
            var info = new Info(userDir, currentDir);
            var result = info.Execute();

            Assert.Equals(result, Result.Ok);
        }

        [Test]
        public void MoveTest()
        {
            var move = new Move(userDir, currentDir);
            var result = move.Execute();

            Assert.Equals(result, Result.Ok);
        }

        [Test]
        public void StaticDataTest()
        {
            var staticData = new StaticData(userDir, currentDir);
            var result = staticData.Execute();

            Assert.Equals(result, Result.Ok);
        }
    }
}