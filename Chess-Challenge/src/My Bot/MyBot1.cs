// using System;
// using System.Linq;
// using System.Diagnostics.CodeAnalysis;
// using ChessChallenge.API;
// using static ChessChallenge.Application.ConsoleHelper;

// public class MyBot1 : IChessBot
// {
//     double[] pieceValues = { 0, 100, 300, 340, 500, 900, 10000 };
//     public Move Think(Board board, Timer timer)
//     {
//         return EvaluateRecursive(board, 3);
//     }

//     public Move ThinkRecursive(Board board, int depth) {
//         Move[] moves = board.GetLegalMoves();
//         double currentValue = EvaluatePosition(board);
//         Move moveToMake = moves[0];
//         foreach(Move move in moves) {
//             board.MakeMove(move);
//             ThinkRecursive(board, depth - 1);

//             board.UndoMove(move);
//         }
//         return moveToMake;
//     }

//     double EvaluatePosition(Board board) {
//         double boardValue = 0;
//         PieceList[] whites = board.GetAllPieceLists().Take(6).ToArray();
//         PieceList[] blacks = board.GetAllPieceLists().Skip(6).ToArray();
//         boardValue += board.IsWhiteToMove ? EvaluateSide(board, whites, true) : EvaluateSide(board, blacks, true);
//         boardValue += board.IsWhiteToMove ? EvaluateSide(board, whites, false) : EvaluateSide(board, blacks, false);
//         return boardValue;
//     }

//     double EvaluateSide(Board board, PieceList[] pieceLists, bool myPiece) {
//         double sideValue = 0;
//         foreach(PieceList pieceList in pieceLists) {
//             foreach(Piece piece in pieceList) {
//                 sideValue += myPiece ? EvaluateMyPiece(board, piece) : EvaluateEnemyPiece(board, piece);
//             }
//         }
//         return sideValue;
//     }
//     double EvaluateMyPiece(Board board, Piece piece) {
//         if(board.SquareIsAttackedByOpponent(piece.Square) {
//             return pieceValues[(int)piece.PieceType] / 2;
//         }
//         return pieceValues[(int)piece.PieceType];
//     }

//     double EvaluateEnemyPiece(Board board, Piece piece) {
        
//         if(board.SquareIsAttackedByOpponent(piece.Square)) {
//             return - pieceValues[(int)piece.PieceType] * 2;
//         }
//         return  -pieceValues[(int)piece.PieceType];
//     }


// }

