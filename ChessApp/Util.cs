using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessModels;

namespace ChessApp {
    public static class Util {

        public static string GetResourceName(this ChessPiece piece) {
            switch (piece) {
                case ChessPiece.BLACK_BISHOP:
                    return "black_bishop";
                case ChessPiece.BLACK_KING:
                    return "black_king";
                case ChessPiece.BLACK_KNIGHT:
                    return "black_knight";
                case ChessPiece.BLACK_PAWN:
                    return "black_pawn";
                case ChessPiece.BLACK_QUEEN:
                    return "black_queen";
                case ChessPiece.BLACK_ROOK:
                    return "black_rook";
                case ChessPiece.WHITE_BISHOP:
                    return "white_bishop";
                case ChessPiece.WHITE_KING:
                    return "white_king";
                case ChessPiece.WHITE_KNIGHT:
                    return "white_knight";
                case ChessPiece.WHITE_PAWN:
                    return "white_pawn";
                case ChessPiece.WHITE_QUEEN:
                    return "white_queen";
                case ChessPiece.WHITE_ROOK:
                    return "white_rook";
                default:
                    return "";
            }
        }

        public static bool IsWhite(this ChessPiece piece) {
            switch (piece) {
                case ChessPiece.WHITE_ROOK:
                    return true;
                case ChessPiece.WHITE_KNIGHT:
                    return true;
                case ChessPiece.WHITE_BISHOP:
                    return true;
                case ChessPiece.WHITE_QUEEN:
                    return true;
                case ChessPiece.WHITE_KING:
                    return true;
                case ChessPiece.WHITE_PAWN:
                    return true;
                default:
                    return false;
            }            
        }

        public static string GetNotation(this ChessResult result) {
            switch (result) {
                case ChessResult.WHITE_WINS:
                    return "1-0";
                case ChessResult.BLACK_WINS:
                    return "0-1";
                case ChessResult.DRAW:
                    return "1/2-1/2";
                default:
                    return "*";
            }
        }
    }
}
