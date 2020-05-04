using System;
using System.Collections.Generic;

namespace ChessModels
{

    public class Location { 
        public int X { get; set; }
        public int Y { get; set; }

        public Location(int x, int y) {
            X = x;
            Y = y;            
        }
        public override bool Equals(object obj) {
            if (obj.GetType() == GetType()) {
                Location loc = (Location)obj;
                if (loc.X == X && loc.Y == Y) {
                    return true;
                }
            }
            return false;
        }
        public Location Clone() {
            return new Location(X, Y);
        }
    }

    public enum ChessPiece { 
        WHITE_ROOK, WHITE_KNIGHT, WHITE_BISHOP, WHITE_QUEEN, WHITE_KING, WHITE_PAWN,
        BLACK_ROOK, BLACK_KNIGHT, BLACK_BISHOP, BLACK_QUEEN, BLACK_KING, BLACK_PAWN, EMPTY
    }


    public class ChessMove {

        public const int NONE = 0, KINGSIDE = 1, QUEENSIDE = 2;

        public ChessMove NextMove { get; set; }
        public ChessMove LastMove { get; set; }
        public string Comment { get; set; }
        public List<ChessMove> Alternatives { get; set; } = new List<ChessMove>();
        public ChessPiece MovedPiece { get; set; }
        public ChessPiece CapturedPiece { get; set; } = ChessPiece.EMPTY;
        public Location StartLocation { get; set; }
        public Location EndLocation { get; set; }
        public ChessPiece Promotion { get; set; }
        public bool EnPassant { get; set; }
        public int Castling { get; set; } = NONE;
        public bool[] LossOfCastling { get; set; } = { false, false, false, false };
        public string Notation { get; set; }

    }
}
