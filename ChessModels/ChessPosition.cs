using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessModels {
    public class ChessPosition {

        public ChessPiece[,] Board { get; set; }

        public ChessMove LastMove { get; set; }

        public bool WhiteToMove { get; set; } = true;


        // 0: White Kingside
        // 1: White Queenside
        // 2: Black Kingside
        // 3: Black Queenside
        public bool[] Castling { get; } = { true, true, true, true };

        public ChessPosition() {
            ChessPiece[,] fakeBoard = new ChessPiece[,]{
                {
                    ChessPiece.WHITE_ROOK, ChessPiece.WHITE_KNIGHT, ChessPiece.WHITE_BISHOP, ChessPiece.WHITE_QUEEN,
                    ChessPiece.WHITE_KING, ChessPiece.WHITE_BISHOP, ChessPiece.WHITE_KNIGHT, ChessPiece.WHITE_ROOK
                },
                {
                    ChessPiece.WHITE_PAWN, ChessPiece.WHITE_PAWN, ChessPiece.WHITE_PAWN, ChessPiece.WHITE_PAWN,
                    ChessPiece.WHITE_PAWN, ChessPiece.WHITE_PAWN, ChessPiece.WHITE_PAWN, ChessPiece.WHITE_PAWN
                },
                {
                    ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY,
                    ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY,ChessPiece.EMPTY
                },
                {
                    ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY,
                    ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY,ChessPiece.EMPTY
                },
                {
                    ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY,
                    ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY,ChessPiece.EMPTY
                },
                {
                    ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY,
                    ChessPiece.EMPTY, ChessPiece.EMPTY, ChessPiece.EMPTY,ChessPiece.EMPTY
                },
                {
                    ChessPiece.BLACK_PAWN, ChessPiece.BLACK_PAWN, ChessPiece.BLACK_PAWN, ChessPiece.BLACK_PAWN,
                    ChessPiece.BLACK_PAWN, ChessPiece.BLACK_PAWN, ChessPiece.BLACK_PAWN, ChessPiece.BLACK_PAWN
                },
                {
                    ChessPiece.BLACK_ROOK, ChessPiece.BLACK_KNIGHT, ChessPiece.BLACK_BISHOP, ChessPiece.BLACK_QUEEN,
                    ChessPiece.BLACK_KING, ChessPiece.BLACK_BISHOP, ChessPiece.BLACK_KNIGHT, ChessPiece.BLACK_ROOK
                },
            };
            Board = new ChessPiece[8, 8];
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    Board[i, j] = fakeBoard[j, i];
                }
            }

            LastMove = new ChessMove();

        }

        public void MakeMoveOnBoard(ChessMove move) {

            if (move.Promotion != ChessPiece.EMPTY) {
                Board[move.EndLocation.X, move.EndLocation.Y] = move.Promotion;
            }
            else {
                Board[move.EndLocation.X, move.EndLocation.Y] =
                    Board[move.StartLocation.X, move.StartLocation.Y];
            }

            Board[move.StartLocation.X, move.StartLocation.Y] = ChessPiece.EMPTY;
            if (move.EnPassant) {
                Board[move.EndLocation.X, move.StartLocation.Y] = ChessPiece.EMPTY;
            }
            else if (move.Castling == ChessMove.QUEENSIDE) {
                Board[0, move.StartLocation.Y] = ChessPiece.EMPTY;
                Board[3, move.StartLocation.Y] = WhiteToMove ? ChessPiece.WHITE_ROOK :
                    ChessPiece.BLACK_ROOK;
            }
            else if (move.Castling == ChessMove.KINGSIDE) {
                Board[7, move.StartLocation.Y] = ChessPiece.EMPTY;
                Board[5, move.StartLocation.Y] = WhiteToMove ? ChessPiece.WHITE_ROOK :
                    ChessPiece.BLACK_ROOK;
            }

            #region Castling Rights
            if (move.MovedPiece == ChessPiece.WHITE_KING) {
                Castling[0] = false;
                Castling[1] = false;
            }
            else if (move.MovedPiece == ChessPiece.BLACK_KING) {
                Castling[2] = false;
                Castling[3] = false;
            }
            else if (move.MovedPiece == ChessPiece.WHITE_ROOK) {
                if (move.StartLocation.X == 0) {
                    Castling[1] = false;
                }
                else if (move.StartLocation.X == 7) {
                    Castling[0] = false;
                }
            }
            else if (move.MovedPiece == ChessPiece.BLACK_ROOK) {
                if (move.StartLocation.X == 0) {
                    Castling[3] = false;
                }
                else if (move.StartLocation.X == 7) {
                    Castling[2] = false;
                }
            }
            #endregion

            if (LastMove != null) {
                LastMove.NextMove = move;
            }
            move.LastMove = LastMove;

            LastMove = move;
            WhiteToMove = !WhiteToMove;
        }

        public void UndoLastMove() {

            if (LastMove.StartLocation == null) {
                return;
            }

            Board[LastMove.StartLocation.X, LastMove.StartLocation.Y] = LastMove.MovedPiece;
            Board[LastMove.EndLocation.X, LastMove.EndLocation.Y] = LastMove.CapturedPiece;

            if (LastMove.EnPassant) {
                ChessPiece addition = IsPieceWhite(LastMove.MovedPiece) ?
                    ChessPiece.BLACK_PAWN : ChessPiece.WHITE_PAWN;
                Board[LastMove.EndLocation.X, LastMove.EndLocation.Y +
                    (IsPieceWhite(LastMove.MovedPiece) ? -1 : 1)] = addition;
            }
            else if (LastMove.Castling != ChessMove.NONE) {
                if (LastMove.EndLocation.Y == 0) {
                    if (LastMove.Castling == ChessMove.QUEENSIDE) {
                        Castling[1] = true;
                        Board[3, 0] = ChessPiece.EMPTY;
                        Board[0, 0] = ChessPiece.WHITE_ROOK;
                    }
                    else {
                        Castling[0] = true;
                        Board[5, 0] = ChessPiece.EMPTY;
                        Board[7, 0] = ChessPiece.WHITE_ROOK;
                    }
                }
                else {
                    if (LastMove.Castling == ChessMove.QUEENSIDE) {
                        Castling[3] = true;
                        Board[3, 7] = ChessPiece.EMPTY;
                        Board[0, 7] = ChessPiece.BLACK_ROOK;
                    }
                    else {
                        Castling[2] = true;
                        Board[5, 7] = ChessPiece.EMPTY;
                        Board[7, 7] = ChessPiece.BLACK_ROOK;
                    }
                }
            }

            if (LastMove.LossOfCastling[0])
                Castling[0] = true;
            if (LastMove.LossOfCastling[1])
                Castling[1] = true;
            if (LastMove.LossOfCastling[2])
                Castling[2] = true;
            if (LastMove.LossOfCastling[3])
                Castling[3] = true;

            LastMove = LastMove.LastMove;
            WhiteToMove = !WhiteToMove;
        }

        public void RedoMove() {
            if (LastMove.NextMove != null) {
                MakeMoveOnBoard(LastMove.NextMove);
            }
        }

        public List<ChessMove> GetLegalMoves() {

            return null;
        }

        public bool IsMoveLegal(ChessMove move) {

            if (move.MovedPiece == ChessPiece.EMPTY)
                return false;
            else if (IsPieceWhite(move.MovedPiece) != WhiteToMove)
                return false;
            else if (move.Castling != ChessMove.NONE) {
                if (IsKingInCheck(WhiteToMove))
                    return false;
                //TODO: implement castling through check
            }
            if (GetLegalDestinations(move.StartLocation).Contains(move.EndLocation)) {
                ChessPosition clone = Clone();
                clone.MakeMoveOnBoard(move);
                if (!clone.IsKingInCheck(WhiteToMove))
                    return true;
            }

            return false;
        }

        private List<Location> GetLegalDestinations(Location start) {
            List<Location> locs = new List<Location>();
            ChessPiece piece = Board[start.X, start.Y];

            if (piece == ChessPiece.WHITE_PAWN) {
                //Forward movement
                if (start.Y < 7 && Board[start.X, start.Y + 1] == ChessPiece.EMPTY) {
                    locs.Add(new Location(start.X, start.Y + 1));
                    if (start.Y == 1 && Board[start.X, start.Y + 2] == ChessPiece.EMPTY) {
                        locs.Add(new Location(start.X, start.Y + 2));
                    }
                }

                //Captures
                if (start.X > 0 && start.Y < 7 && Board[start.X - 1, start.Y + 1] != ChessPiece.EMPTY
                        && !IsPieceWhite(Board[start.X - 1, start.Y + 1]))
                    locs.Add(new Location(start.X - 1, start.Y + 1));
                if (start.X < 7 && start.Y < 7 && Board[start.X + 1, start.Y + 1] != ChessPiece.EMPTY
                        && !IsPieceWhite(Board[start.X + 1, start.Y + 1]))
                    locs.Add(new Location(start.X + 1, start.Y + 1));

                //En passant
                if (LastMove != null && LastMove.MovedPiece == ChessPiece.BLACK_PAWN) {
                    if (Math.Abs(LastMove.StartLocation.X - start.X) == 1) {
                        if (LastMove.StartLocation.Y == 6 & LastMove.EndLocation.Y == 4 && start.Y == 4) {
                            locs.Add(new Location(LastMove.StartLocation.X, start.Y + 1));
                        }
                    }
                }
            }
            else if (piece == ChessPiece.BLACK_PAWN) {

                //Forward
                if (start.Y > 0 && Board[start.X, start.Y - 1] == ChessPiece.EMPTY) {
                    locs.Add(new Location(start.X, start.Y - 1));
                    if (start.Y == 6 && Board[start.X, start.Y - 2] == ChessPiece.EMPTY) {
                        locs.Add(new Location(start.X, start.Y - 2));
                    }
                }

                //Captures
                if (start.X > 0 && start.Y > 0 && Board[start.X - 1, start.Y - 1] != ChessPiece.EMPTY
                        && IsPieceWhite(Board[start.X - 1, start.Y - 1]))
                    locs.Add(new Location(start.X - 1, start.Y - 1));
                if (start.X < 7 && start.Y > 0 && Board[start.X + 1, start.Y - 1] != ChessPiece.EMPTY
                        && IsPieceWhite(Board[start.X + 1, start.Y - 1]))
                    locs.Add(new Location(start.X + 1, start.Y - 1));

                //En passant
                if (LastMove != null && LastMove.MovedPiece == ChessPiece.WHITE_PAWN) {
                    if (Math.Abs(LastMove.StartLocation.X - start.X) == 1) {
                        if (LastMove.StartLocation.Y == 1 & LastMove.EndLocation.Y == 3 && start.Y == 3) {
                            locs.Add(new Location(LastMove.StartLocation.X, start.Y - 1));
                        }
                    }
                }
            }
            else if (piece == ChessPiece.WHITE_ROOK || piece == ChessPiece.BLACK_ROOK) {
                for (int i = start.X + 1; i < 8; i++) {
                    Location potLoc = new Location(i, start.Y);
                    ChessPiece p = Board[potLoc.X, potLoc.Y];
                    if (p == ChessPiece.EMPTY)
                        locs.Add(potLoc);
                    else {
                        if (IsPieceWhite(p) != IsPieceWhite(piece))
                            locs.Add(potLoc);
                        break;
                    }

                }
                for (int j = start.X - 1; j >= 0; j--) {
                    Location potLoc = new Location(j, start.Y);
                    ChessPiece p = Board[potLoc.X, potLoc.Y];
                    if (p == ChessPiece.EMPTY)
                        locs.Add(potLoc);
                    else {
                        if (IsPieceWhite(p) != IsPieceWhite(piece))
                            locs.Add(potLoc);
                        break;
                    }
                }
                for (int k = start.Y + 1; k < 8; k++) {
                    Location potLoc = new Location(start.X, k);
                    ChessPiece p = Board[potLoc.X, potLoc.Y];
                    if (p == ChessPiece.EMPTY)
                        locs.Add(potLoc);
                    else {
                        if (IsPieceWhite(p) != IsPieceWhite(piece))
                            locs.Add(potLoc);
                        break;
                    }
                }
                for (int l = start.Y - 1; l >= 0; l--) {
                    Location potLoc = new Location(start.X, l);
                    ChessPiece p = Board[potLoc.X, potLoc.Y];
                    if (p == ChessPiece.EMPTY)
                        locs.Add(potLoc);
                    else {
                        if (IsPieceWhite(p) != IsPieceWhite(piece))
                            locs.Add(potLoc);
                        break;
                    }
                }
            }
            else if (piece == ChessPiece.WHITE_KNIGHT || piece == ChessPiece.BLACK_KNIGHT) {
                Location[] potLocs = new Location[]{
                    new Location(start.X + 1, start.Y + 2),
                    new Location(start.X - 1, start.Y + 2),
                    new Location(start.X + 2, start.Y + 1),
                    new Location(start.X + 2, start.Y - 1),
                    new Location(start.X - 2, start.Y + 1),
                    new Location(start.X - 2, start.Y - 1),
                    new Location(start.X + 1, start.Y - 2),
                    new Location(start.X - 1, start.Y - 2)
            };

                foreach (Location l in potLocs) {
                    if (l.X > 7 || l.X < 0 || l.Y > 7 || l.Y < 0)
                        continue;
                    ChessPiece p = Board[l.X, l.Y];
                    if (p == ChessPiece.EMPTY) {
                        locs.Add(l);
                        continue;
                    }
                    if (IsPieceWhite(p) != IsPieceWhite(piece))
                        locs.Add(l);
                }
            }
            else if (piece == ChessPiece.WHITE_BISHOP || piece == ChessPiece.BLACK_BISHOP) {

                int rank = start.X;
                int file = start.Y;

                for (int i = rank + 1, j = file + 1; i < 8 && j < 8; i++, j++) {
                    Location potLoc = new Location(i, j);
                    if (Board[potLoc.X, potLoc.Y] == ChessPiece.EMPTY) {
                        locs.Add(potLoc);
                    }
                    else {
                        if (IsPieceWhite(Board[potLoc.X, potLoc.Y]) != IsPieceWhite(Board[rank, file])) {
                            locs.Add(potLoc);
                        }
                        break;
                    }
                }
                for (int i = rank + 1, j = file - 1; i < 8 && j >= 0; i++, j--) {
                    Location potLoc = new Location(i, j);
                    if (Board[potLoc.X, potLoc.Y] == ChessPiece.EMPTY) {
                        locs.Add(potLoc);
                    }
                    else {
                        if (IsPieceWhite(Board[potLoc.X, potLoc.Y]) != IsPieceWhite(Board[rank, file])) {
                            locs.Add(potLoc);
                        }
                        break;
                    }
                }
                for (int i = rank - 1, j = file + 1; i >= 0 && j < 8; i--, j++) {
                    Location potLoc = new Location(i, j);
                    if (Board[potLoc.X, potLoc.Y] == ChessPiece.EMPTY) {
                        locs.Add(potLoc);
                    }
                    else {
                        if (IsPieceWhite(Board[potLoc.X, potLoc.Y]) != IsPieceWhite(Board[rank, file])) {
                            locs.Add(potLoc);
                        }
                        break;
                    }
                }
                for (int i = rank - 1, j = file - 1; i >= 0 && j >= 0; i--, j--) {
                    Location potLoc = new Location(i, j);
                    if (Board[potLoc.X, potLoc.Y] == ChessPiece.EMPTY) {
                        locs.Add(potLoc);
                    }
                    else {
                        if (IsPieceWhite(Board[potLoc.X, potLoc.Y]) != IsPieceWhite(Board[rank, file])) {
                            locs.Add(potLoc);
                        }
                        break;
                    }
                }


            }
            else if (piece == ChessPiece.WHITE_QUEEN || piece == ChessPiece.BLACK_QUEEN) {

                Board[start.X, start.Y] = IsPieceWhite(piece) ? ChessPiece.WHITE_BISHOP : ChessPiece.BLACK_BISHOP;
                locs.AddRange(GetLegalDestinations(start));
                Board[start.X, start.Y] = IsPieceWhite(piece) ? ChessPiece.WHITE_ROOK : ChessPiece.BLACK_ROOK;
                locs.AddRange(GetLegalDestinations(start));
                Board[start.X, start.Y] = piece;
            }
            else if (piece == ChessPiece.WHITE_KING || piece == ChessPiece.BLACK_KING) {
                Location[] potLocs = new Location[]{
                    new Location(start.X + 1, start.Y),
                    new Location(start.X + 1, start.Y + 1),
                    new Location(start.X, start.Y + 1),
                    new Location(start.X - 1, start.Y + 1),
                    new Location(start.X - 1, start.Y),
                    new Location(start.X - 1, start.Y - 1),
                    new Location(start.X, start.Y - 1),
                    new Location(start.X + 1, start.Y - 1)
                };

                foreach (Location l in potLocs) {
                    if (l.X > 7 || l.X < 0 || l.Y > 7 || l.Y < 0)
                        continue;
                    if (Board[l.X, l.Y] == ChessPiece.EMPTY) {
                        locs.Add(l);
                    }
                    else if (IsPieceWhite(Board[l.X, l.Y]) != IsPieceWhite(piece))
                        locs.Add(l);
                }

                //Castling
                if (IsPieceWhite(piece)) {
                    if (Castling[0]) {
                        if (Board[start.X + 1, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X + 2, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X + 3, start.Y] == ChessPiece.WHITE_ROOK)
                            locs.Add(new Location(start.X + 2, start.Y));
                    }
                    if (Castling[1]) {
                        if (Board[start.X - 1, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X - 2, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X - 3, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X - 4, start.Y] == ChessPiece.WHITE_ROOK)
                            locs.Add(new Location(start.X - 2, start.Y));
                    }
                }
                else {
                    if (Castling[2]) {
                        if (Board[start.X + 1, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X + 2, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X + 3, start.Y] == ChessPiece.BLACK_ROOK)
                            locs.Add(new Location(start.X + 2, start.Y));
                    }
                    if (Castling[3]) {
                        if (Board[start.X - 1, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X - 2, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X - 3, start.Y] == ChessPiece.EMPTY &&
                                Board[start.X - 4, start.Y] == ChessPiece.BLACK_ROOK)
                            locs.Add(new Location(start.X - 2, start.Y));
                    }
                }
            }
            return locs;
        }

        public ChessMove BuildMove(Location startSquare, Location endSquare, ChessPiece promotion) {

            ChessMove move = new ChessMove();
            move.StartLocation = startSquare;
            move.EndLocation = endSquare;
            move.MovedPiece = Board[startSquare.X, startSquare.Y];
            move.CapturedPiece = Board[endSquare.X, endSquare.Y];
            move.Castling = ChessMove.NONE;
            move.EnPassant = false;
            move.Promotion = promotion;

            if (move.MovedPiece == ChessPiece.WHITE_KING || move.MovedPiece == ChessPiece.BLACK_KING) {
                int dist = endSquare.X - startSquare.X;
                if (dist > 1) {
                    move.Castling = ChessMove.KINGSIDE;
                }
                else if (dist < -1) {
                    move.Castling = ChessMove.QUEENSIDE;
                }
                else {
                    if (IsPieceWhite(move.MovedPiece)) {
                        move.LossOfCastling[0] = Castling[0];
                        move.LossOfCastling[1] = Castling[1];
                    }
                    else {
                        move.LossOfCastling[2] = Castling[2];
                        move.LossOfCastling[3] = Castling[3];
                    }
                }
            }
            else if (move.MovedPiece == ChessPiece.WHITE_ROOK || move.MovedPiece == ChessPiece.BLACK_ROOK) {
                if (startSquare.X == 0 && startSquare.Y == 0) {
                    move.LossOfCastling[1] = Castling[1];
                }
                else if (startSquare.X == 7 && startSquare.Y == 0) {
                    move.LossOfCastling[0] = Castling[0];
                }
                else if (startSquare.X == 0 && startSquare.Y == 7) {
                    move.LossOfCastling[3] = Castling[3];
                }
                else if (startSquare.X == 7 && startSquare.Y == 7) {
                    move.LossOfCastling[2] = Castling[2];
                }
            }
            else if (move.MovedPiece == ChessPiece.WHITE_PAWN || move.MovedPiece == ChessPiece.BLACK_PAWN) {
                if (move.CapturedPiece == ChessPiece.EMPTY && startSquare.X != endSquare.X) {
                    move.EnPassant = true;
                }
            }
            move.Notation = GetNotation(move);
            return move;
        }

        public ChessMove BuildMove(string text) {
            ChessMove move = new ChessMove();
            move.EnPassant = false;
            move.Notation = text;
            move.Promotion = ChessPiece.EMPTY;

            #region Castling
            if (text == "O-O-O") {
                move.Castling = ChessMove.QUEENSIDE;
                if (WhiteToMove) {
                    move.LossOfCastling[0] = true;
                    move.LossOfCastling[1] = true;
                    move.MovedPiece = ChessPiece.WHITE_KING;
                    move.StartLocation = new Location(4, 0);
                    move.EndLocation = new Location(2, 0);
                    return move;
                }
                else {
                    move.MovedPiece = ChessPiece.BLACK_KING;
                    move.LossOfCastling[2] = true;
                    move.LossOfCastling[3] = true;
                    move.StartLocation = new Location(4, 7);
                    move.EndLocation = new Location(2, 7);
                    return move;
                }
            }
            else if (text == "O-O") {
                move.Castling = ChessMove.KINGSIDE;
                if (WhiteToMove) {
                    move.MovedPiece = ChessPiece.WHITE_KING;
                    move.LossOfCastling[0] = true;
                    move.LossOfCastling[1] = true;
                    move.StartLocation = new Location(4, 0);
                    move.EndLocation = new Location(6, 0);
                    return move;
                }
                else {
                    move.MovedPiece = ChessPiece.BLACK_KING;
                    move.LossOfCastling[2] = true;
                    move.LossOfCastling[3] = true;
                    move.StartLocation = new Location(4, 7);
                    move.EndLocation = new Location(6, 7);
                    return move;
                }
            }
            #endregion
            else {
                if (text.Contains("=")) {
                    move.Promotion = PieceFromLetter(text.Split('=')[1][0], WhiteToMove);
                }
                move.MovedPiece = PieceFromLetter(text[0], WhiteToMove);
                int index = text.Length - 1;
                int rank = -1;

                while (!char.IsDigit(text[index])) {
                    index--;
                }
                rank = int.Parse("" + text[index]);
                int file = ColumnFromLetter(text[index - 1]);
                if (rank == -1) {
                    return null;
                }
                rank--;

                Location destination = new Location(file, rank);

                for (int i = 0; i < 8; i++) {
                    for (int j = 0; j < 8; j++) {
                        if (Board[i, j] == move.MovedPiece) {
                            Location potLoc = new Location(i, j);
                            string plainMove = text.Substring(0, index + 1);
                            if (plainMove.Length >= 4 && plainMove[1] != 'x') {
                                if (char.IsLetter(plainMove[1])) {
                                    int fileReq = ColumnFromLetter(plainMove[1]);
                                    if (i != fileReq)
                                        continue;
                                }
                                else if (char.IsDigit(plainMove[1])) {
                                    int rankReq = int.Parse("" + plainMove[1]) - 1;
                                    if (j != rankReq)
                                        continue;
                                }
                            }
                            else if (move.MovedPiece == ChessPiece.WHITE_PAWN
                                || move.MovedPiece == ChessPiece.BLACK_PAWN) {
                                if (plainMove[1] == 'x') {
                                    if (!(potLoc.X == ColumnFromLetter(plainMove[0]))) {
                                        continue;
                                    }
                                }
                            }
                            if (GetLegalDestinations(potLoc).Contains(destination)) {
                                return BuildMove(potLoc, destination, move.Promotion);
                            }
                        }
                    }
                }
            }


            return null;
        }

        public string GetNotation(ChessMove move) {
            string output = "";

            ChessPiece movedPiece = move.MovedPiece;
            ChessPiece promotion = move.Promotion;

            if (movedPiece == ChessPiece.WHITE_KING || movedPiece == ChessPiece.BLACK_KING) {
                if (Math.Abs(move.EndLocation.X - move.StartLocation.X) > 1) {
                    if (move.EndLocation.X > move.StartLocation.X) {
                        return "O-O";
                    }
                    else {
                        return "O-O-O";
                    }
                }
            }

            string rankNum = (move.EndLocation.Y + 1).ToString();
            char fileLet = LetterFromFile(move.EndLocation.X);
            string pieceLet = LetterFromPiece(movedPiece);

            bool capture = Board[move.EndLocation.X, move.EndLocation.Y] != ChessPiece.EMPTY;
            output += pieceLet;


            if (capture && (movedPiece == ChessPiece.WHITE_PAWN || movedPiece == ChessPiece.BLACK_PAWN)) {
                output += LetterFromFile(move.StartLocation.X);
            }


            if (movedPiece == ChessPiece.BLACK_KNIGHT || movedPiece == ChessPiece.WHITE_KNIGHT ||
                    movedPiece == ChessPiece.BLACK_ROOK || movedPiece == ChessPiece.WHITE_ROOK ||
                    movedPiece == ChessPiece.WHITE_QUEEN || movedPiece == ChessPiece.BLACK_QUEEN) {
                for (int i = 0; i < 8; i++) {
                    for (int j = 0; j < 8; j++) {
                        Location loc = new Location(i, j);
                        if (Board[i, j] == movedPiece && !loc.Equals(move.StartLocation)) {
                            if (GetLegalDestinations(loc).Contains(move.EndLocation)) {
                                if (j != move.StartLocation.X) {
                                    output += LetterFromFile(move.StartLocation.X);
                                    break;
                                }
                                else {
                                    output += move.StartLocation.Y + 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (capture) {
                output += 'x';
            }

            output += fileLet;
            output += rankNum;

            if (promotion != ChessPiece.EMPTY) {
                output += '=' + LetterFromPiece(promotion);
            }


            return output;
        }

        private bool IsKingInCheck(bool whiteKing) {

            int kingRank = -1;
            int kingFile = -1;

            List<Location> attackers = new List<Location>();

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {

                    if (Board[i, j] == ChessPiece.WHITE_KING || Board[i, j] == ChessPiece.BLACK_KING) {
                        if (IsPieceWhite(Board[i, j]) == whiteKing) {
                            kingRank = i;
                            kingFile = j;
                            continue;
                        }
                    }

                    if (Board[i, j] != ChessPiece.EMPTY && IsPieceWhite(Board[i, j]) != whiteKing) {
                        attackers.Add(new Location(i, j));
                    }
                }

                foreach (Location loc in attackers) {
                    if (GetLegalDestinations(loc).Contains(new Location(kingRank, kingFile))) {
                        return true;
                    }
                }
            }

            return false;
        }

        #region Helper Methods

        public ChessPosition Clone() {
            ChessPosition pos = new ChessPosition();
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    pos.Board[i, j] = Board[i, j];
                }
            }
            if (LastMove.StartLocation != null) {
                pos.LastMove = new ChessMove() {
                    StartLocation = LastMove.StartLocation.Clone(),
                    EndLocation = LastMove.EndLocation.Clone(),
                    Promotion = LastMove.Promotion,
                    EnPassant = LastMove.EnPassant,
                    Castling = LastMove.Castling
                };
            }
            pos.WhiteToMove = WhiteToMove;
            for (int i = 0; i < 4; i++) {
                pos.Castling[i] = Castling[i];
            }

            return pos;
        }

        private static char LetterFromFile(int file) {
            switch (file) {
                case 0:
                    return 'a';
                case 1:
                    return 'b';
                case 2:
                    return 'c';
                case 3:
                    return 'd';
                case 4:
                    return 'e';
                case 5:
                    return 'f';
                case 6:
                    return 'g';
                case 7:
                    return 'h';
                default:
                    return 'i';
            }
        }

        private static string LetterFromPiece(ChessPiece piece) {
            if (piece == ChessPiece.WHITE_ROOK || piece == ChessPiece.BLACK_ROOK) {
                return "R";
            }
            else if (piece == ChessPiece.WHITE_KNIGHT || piece == ChessPiece.BLACK_KNIGHT) {
                return "N";
            }
            else if (piece == ChessPiece.WHITE_BISHOP || piece == ChessPiece.BLACK_BISHOP) {
                return "B";
            }
            else if (piece == ChessPiece.WHITE_QUEEN || piece == ChessPiece.BLACK_QUEEN) {
                return "Q";
            }
            else if (piece == ChessPiece.WHITE_KING || piece == ChessPiece.BLACK_KING) {
                return "K";
            }
            else {
                return "";
            }
        }

        private static ChessPiece PieceFromLetter(char letter, bool white) {

            if (letter == 'R' && white) {
                return ChessPiece.WHITE_ROOK;
            }
            else if (letter == 'R' && !white) {
                return ChessPiece.BLACK_ROOK;
            }
            else if (letter == 'N' && white) {
                return ChessPiece.WHITE_KNIGHT;
            }
            else if (letter == 'N' && !white) {
                return ChessPiece.BLACK_KNIGHT;
            }
            else if (letter == 'B' && white) {
                return ChessPiece.WHITE_BISHOP;
            }
            else if (letter == 'B' && !white) {
                return ChessPiece.BLACK_BISHOP;
            }
            else if (letter == 'Q' && white) {
                return ChessPiece.WHITE_QUEEN;
            }
            else if (letter == 'Q' && !white) {
                return ChessPiece.BLACK_QUEEN;
            }
            else if (letter == 'K' && white) {
                return ChessPiece.WHITE_KING;
            }
            else if (letter == 'K' && !white) {
                return ChessPiece.BLACK_KING;
            }
            else if (white) {
                return ChessPiece.WHITE_PAWN;
            }
            else {
                return ChessPiece.BLACK_PAWN;
            }
        }

        private static int ColumnFromLetter(char letter) {
            switch (letter) {
                case 'a':
                    return 0;
                case 'b':
                    return 1;
                case 'c':
                    return 2;
                case 'd':
                    return 3;
                case 'e':
                    return 4;
                case 'f':
                    return 5;
                case 'g':
                    return 6;
                case 'h':
                    return 7;
                default:
                    return -1;
            }
        }

        public static bool IsPieceWhite(ChessPiece piece) {

            switch (piece) {
                case ChessPiece.WHITE_PAWN:
                case ChessPiece.WHITE_ROOK:
                case ChessPiece.WHITE_KNIGHT:
                case ChessPiece.WHITE_BISHOP:
                case ChessPiece.WHITE_QUEEN:
                case ChessPiece.WHITE_KING:
                    return true;
                default:
                    return false;
            }
        }
        #endregion
    }
}
