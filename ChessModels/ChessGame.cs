using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessModels {

    public enum ChessResult { 
        WHITE_WINS, BLACK_WINS, DRAW, UNAVAILABLE
    }


    public class ChessGame {

        public ChessMove Moves { get; set; }
        
        public string Event { get; set; }

        public string Site { get; set; }

        public DateTime Date { get; set; }

        public string Round { get; set; }

        public string WhitePlayer { get; set; }

        public string BlackPlayer { get; set; }

        public ChessResult Result { get; set; }

        public void AddTagPair(string tagName, string value) {
            value = value.Substring(1, value.Length - 2);
            if (tagName == "Event") {
                Event = value;
            }
            else if (tagName == "Site") {
                Site = value;
            }
            else if (tagName == "Date") {
                Date = DateTime.Parse(value);
            }
            else if (tagName == "Round") {
                Round = value;
            }
            else if (tagName == "White") {
                WhitePlayer = value;
            }
            else if (tagName == "Black") {
                BlackPlayer = value;
            }
            else if (tagName == "Result") {
                if (value == "0-1") {
                    Result = ChessResult.BLACK_WINS;
                }
                else if (value == "1-0") {
                    Result = ChessResult.WHITE_WINS;
                }
                else if (value == "1/2-1/2") {
                    Result = ChessResult.DRAW;
                }
                else {
                    Result = ChessResult.UNAVAILABLE;
                }
            }
        }

        public static ChessGame LoadFromPGN(string pgn) {
            ChessGame game = new ChessGame();
            List<string> tokens = new List<string>();
            string token = "";
            foreach (char c in pgn) {
                if (char.IsWhiteSpace(c)) {
                    if (token.Length > 0) {
                        tokens.Add(token);
                        token = "";
                    }
                }
                else {
                    token += c;
                }
            }
            if (token.Length > 0) {
                tokens.Add(token);
            }

            bool inSideLine = false;
            bool inComment = false;
            string sideLine = "";
            string comment = "";

            for (int i = 0; i < tokens.Count; i++) {
                if (tokens[i][0] == '[') {
                    string tagName = tokens[i].Substring(1);
                    string value = tokens[++i].Substring(0, tokens[i].Length - 1);
                    game.AddTagPair(tagName, value);
                    continue;
                }
                else if (tokens[i][0] == '(') {
                    //Annotation moves
                }
                else if (tokens[i][0] == '{') {
                    //Comment
                }
                else if (char.IsDigit(tokens[i][0])) {
                    //Move number
                }
                else if (char.IsLetter(tokens[i][0])) { 
                    //Move
                }
            }

            return game;
        }

        public ChessMove ParseMoves(ChessPosition startingPosition, List<string> tokens) {
            return null;
        }

    }
}
