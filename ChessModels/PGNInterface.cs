using System;
using System.Collections.Generic;
using System.Text;

namespace ChessModels {

    enum TokenType { 
        TAG_PAIR, MOVE_NUMBER, COMMENT, ANNOTATION, MOVE, RESULT, EMPTY
    }

    class Token {
        public TokenType Type { get; set; }
        public string Text { get; set; }
    }

    public static class PGNInterface {

        static List<char> _selfTerminating = 
            new List<char>() { '[', ']', '(', ')', '.', '*' };

        public static ChessGame LoadFromPGN(string pgnText) {

            List<string> tokens = new List<string>();

            bool stringToken = false;
            bool commentToken = false;
            bool escape = false;
            string text = "";

            foreach (char c in pgnText) {

                if (_selfTerminating.Contains(c) && !stringToken && !commentToken) {
                    if (text.Length > 0) {
                        tokens.Add(text);
                        text = "";
                    }
                    tokens.Add(c.ToString());
                }
                else if (c == '\\') {
                    escape = true;
                }
                else if (c == '"') {
                    if (!escape) {
                        stringToken = !stringToken;
                        if (!stringToken) {
                            tokens.Add("\"" + text + "\"");
                            text = "";
                        }
                    }
                    else {
                        tokens.Add("\"");
                        escape = false;
                    }
                }
                else if (c == '{') {
                    commentToken = true;
                }
                else if (c == '}') {
                    commentToken = false;
                    if (text.Length > 0) {
                        tokens.Add("{" + text + "}");
                        text = "";
                    }
                }
                else if (!char.IsWhiteSpace(c) || stringToken || commentToken) {
                    if (char.IsWhiteSpace(c)) {
                        text += " ";
                    }
                    else {
                        text += c;
                    }
                }
                else if (char.IsWhiteSpace(c)) {
                    if (text.Length > 0) {
                        tokens.Add(text);
                        text = "";
                    }
                }
            }
            if (text.Length > 0) {
                tokens.Add(text);
                text = "";
            }

            ChessGame game = new ChessGame();
            ChessPosition builder = new ChessPosition();

            bool tagPair = false;
            string name = "";
            string value = "";
            List<string> moveTokens = new List<string>();

            Dictionary<string, ChessResult> results = new Dictionary<string, ChessResult>();
            results["1-0"] = ChessResult.WHITE_WINS;
            results["0-1"] = ChessResult.BLACK_WINS;
            results["1/2-1/2"] = ChessResult.DRAW;
            results["*"] = ChessResult.UNAVAILABLE;

            foreach (string token in tokens) {
                if (token == "[") {
                    tagPair = true;
                }
                else if (token == "]") {
                    tagPair = false;
                    game.AddTagPair(name, value);
                    name = "";
                    value = "";
                }
                else if (tagPair) {
                    if (name.Length < 1) {
                        name = token;
                    }
                    else {
                        value = token;
                    }
                }
                else if (results.ContainsKey(token)) {
                    break;
                }
                else {
                    moveTokens.Add(token);
                }

            }

            game.Moves = ParseTokens(builder, moveTokens);

            return game;
        }

        private static ChessMove ParseTokens( ChessPosition currentPosition, List<string> tokens) {

            ChessMove currentMove = null;
            List<string> annotationTokens = new List<string>();
            bool inAnnotation = false;
            int testNum = 0;

            foreach (string token in tokens) {
                if (token == "(") {
                    inAnnotation = true;
                }
                else if (token == ")") {
                    inAnnotation = false;
                    if (currentMove != null) {
                        currentMove.Alternatives.Add(ParseTokens(currentPosition.Clone(), annotationTokens));
                        annotationTokens.Clear();
                    }
                }
                else if (inAnnotation) {
                    annotationTokens.Add(token);
                }
                else if (token[0] == '{') {
                    if (currentMove != null) {
                        currentMove.Comment = token.Substring(1, token.Length - 2);
                    }
                }
                else if (int.TryParse(token, out testNum)) {
                    continue;
                }
                else {
                    bool isMove = true;
                    List<char> special = new List<char>() {
                        '_', '+', '#', '=', ':', '-'
                    };
                    foreach (char c in token) {
                        if (!char.IsDigit(c) && !char.IsLetter(c) && !special.Contains(c)) {
                            isMove = false;
                        }
                    }

                    if (isMove) {
                        if (currentMove != null) {
                            currentPosition.MakeMoveOnBoard(currentMove);
                        }
                        ChessMove newMove = currentPosition.BuildMove(token);
                        newMove.LastMove = currentMove;
                        if (currentMove != null) {
                            currentMove.NextMove = newMove;
                        }
                        currentMove = newMove;
                    }
                }
            }

            while (currentMove.LastMove != null) {
                currentMove = currentMove.LastMove;
            }

            return currentMove;
        }

    }
}
