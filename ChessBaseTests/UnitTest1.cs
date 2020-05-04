using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessModels;
using System.IO;

namespace ChessBaseTests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            ChessPosition pos = new ChessPosition();
            Assert.AreEqual(pos.Board[0, 0], ChessPiece.WHITE_ROOK);
            string text = File.ReadAllText("Output.pgn");
            ChessGame result = PGNInterface.LoadFromPGN(text);
        }
    }
}
