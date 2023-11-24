﻿using System.Linq;
using ChessChallenge.API;

public class MyBot : IChessBot
{
    int[] PieceValues = { 0, 100, 300, 340, 500, 900, 1500 };
    public Move Think(Board board, Timer timer)
    {
        double bestValue = 10000000;
        Move moveToMake = board.GetLegalMoves()[0];
        foreach(Move move in board.GetLegalMoves()) {
            board.MakeMove(move);
            double positionValue = EvaluatePosition(board);
            if (positionValue < bestValue) {
                bestValue = positionValue;
                moveToMake = move;
            }
            board.UndoMove(move);
        }
        return moveToMake;
    }

    double EvaluatePosition(Board board) {
        PieceList[] myPieces = board.IsWhiteToMove ?  board.GetAllPieceLists().Take(6).ToArray() : board.GetAllPieceLists().Skip(6).ToArray();
        PieceList[] enemyPieces = board.IsWhiteToMove ?  board.GetAllPieceLists().Skip(6).ToArray() : board.GetAllPieceLists().Take(6).ToArray();
        double boardValue = EvaluateSide(board, myPieces) - EvaluateSide(board, enemyPieces);
        return boardValue;
    }

    double EvaluateSide(Board board, PieceList[] pieceLists) {
        double sideValue = 0;
        foreach(PieceList pieceList in pieceLists) {
            foreach(Piece piece in pieceList) {
                sideValue += EvaluatePiece(board, piece);
            }
        }
        return sideValue;
    }
    double EvaluatePiece(Board board, Piece piece) {
        return PieceValues[(int)piece.PieceType] * (piece.IsWhite ? PositionWeights[(int)piece.PieceType-1][piece.Square.Index] : PositionWeights[(int)piece.PieceType-1][63-piece.Square.Index]);
    }



    readonly double[][] PositionWeights = new double[6][] {
        new double[64] {1, 1, 1, 1, 1, 1, 1, 1, 1.063947921887923, 1.0541445279390946, 1.1410782185620625, 1.0635353611492695, 1.1233676181515895, 1.085859300618734, 0.9651645896523882, 1.0142728226578297, 1.3229812033580988, 1.122419308882682, 1.229880330452522, 1.157611485429032, 1.1849600651017764, 1.1669669976569563, 0.8936543090523074, 0.9584498695570051, 1.2326450711526595, 1.602168714890354, 1.175947435685143, 1.2100471607284213, 2.3209363623469503, 0.8455890675173623, 1.5030649868247665, 0.9048597009646016, 1.187400892012069, 0.7854853425464864, 1.1706926002723945, 2.236831477151384, 0.7040107764620559, 0.10821990790564673, 0.6054561188933867, 0.6552103473905767, 1.1581667335298709, 1.2117043336548747, 1.6579229632001582, 1.26837330653324, 0.9687187308495293, 1.1430860307460293, 0.8524079609631753, 0.9014338342892522, 2.73118239832356, 3.2499664439719207, 2.336924578879449, 0.49936668816425317, 1.0145977895727418, 1.7705627474091443, 3.4225837320859416, 2.9118951344474477, 1, 1, 1, 1, 1, 1, 1, 1},
        new double[64] {1.0202563324885752, 1.0080227380528783, 1.013916791306923, 0.9980438348845423, 0.9972947441372614, 1.0049121072388347, 0.9946647132034281, 1.0106146032187677, 1.001680684510707, 1.0047094160734016, 1.0295167894730133, 1.0024074533796157, 1.003700755416738, 0.988269733321494, 0.9968403801255907, 0.9986562848739098, 1.0079833595995762, 1.03729607977383, 1.0221685546499584, 1.0249865899391473, 1.0835366636726005, 0.9926261122019339, 0.9894707174653664, 0.983973644766136, 0.9992561901161198, 1.0379262691417386, 1.0557057338034723, 1.1336854298456436, 1.0621714518520875, 1.0350120987203586, 0.9209592512931765, 1.0037035517564836, 1.003600057827668, 0.998157617135172, 1.0312833604578906, 1.0438416764794725, 1.0940073173774736, 0.9936241695748275, 1.0034734044660987, 0.9543941524546585, 0.9376172449067266, 0.9923098607823659, 1.2606772435580784, 0.9964777586898389, 1.0317145546851556, 1.7795819872617584, 0.9724777726443117, 0.9679095584721824, 0.9938608768532621, 1.0005182230877887, 0.9992551925468298, 1.0639260509221093, 0.9975536456310911, 0.9397775328332775, 0.9975969973095399, 0.9781161618865354, 0.9987124595319288, 1.0098585164348384, 1.0004037342440222, 0.9968796731238397, 0.9678513473145943, 0.9732961604479856, 0.9412365438424477, 0.9837798608110886},
        new double[64] {1.0261084080344103, 1.0017599420643568, 1.013250446990764, 1.0035908528486293, 0.998422489220211, 1.02595009419327, 1.0091901446402558, 1.0188065254396728, 0.9964119852726436, 1.054723824866275, 0.9927863891235252, 1.003594416764278, 1.0574282275307338, 1.0066224793168985, 1.012427813024375, 1.0157314937151989, 0.9992543262637826, 1.03708446898277, 1.0837965908327938, 1.0164616580931876, 1.043147916208373, 1.0638610584132728, 0.9944923289600821, 0.9674049329665836, 1.0081707261491393, 1.0206065190000768, 1.035029441139285, 1.0973394469462576, 1.0612263261314179, 1.0309487742441952, 1.0694540179222067, 1.0026914480818143, 0.9909878579199711, 1.0330648484144636, 1.0837591987309925, 1.0623179418652604, 1.082371694156884, 1.0649023111608886, 1.0184857145946682, 1.0127740589102878, 0.9740620685099469, 1.040270576396457, 1.049026251493971, 1.2628994571410004, 1.2790482290438732, 1.0687759086439486, 1.0199136921880925, 1.0257540392380038, 1.03467470099138, 1.0377086520821073, 1.0285419114158318, 0.9125398480011141, 1.1150754931635767, 1.0192163148239817, 1.1210047428362373, 0.9996766037023361, 1.001741051513445, 0.9971060454712373, 0.9170056527646088, 0.9405466748936525, 0.9591078647129039, 0.9551718058764243, 0.9990217569026217, 1.0052374658776302},
        new double[64] {1.0208727342663069, 1.0328876945473568, 1.002843595936097, 1.0279727541305719, 1.0159868536011742, 1.0126905840588116, 1.0008591123314012, 1.0053322274372865, 1.0575139180910433, 1.038001190434461, 1.0299600138636145, 1.021255941749366, 1.0259873045737165, 1.030158980638711, 1.0052098868492059, 1.0177243442684898, 1.0273414623331487, 1.0164435853852964, 1.0151202963911097, 0.9974780164900814, 0.9986493741051207, 1.0124776328006861, 1.0009666292851267, 1.010256092377775, 1.0062327253687513, 1.0313464299258441, 1.0148623836351376, 0.9997512522819897, 1.0048103156581578, 1.0174932701013242, 0.9920512216701628, 1.0034709014572576, 0.9994772470145858, 1.0040822574514463, 0.9924683471389619, 1.0007483374344461, 0.9975388407419147, 0.9855229084285319, 0.9919966668080982, 0.9955774756857124, 1.0065734849591739, 0.9941334783699358, 0.9919060330027005, 0.9839062259520217, 1.00995988349717, 0.9890620139369666, 0.9940325554893271, 0.9886007306218405, 0.9969200296304659, 0.9712100859693573, 0.9865702778508053, 1.0022525845506305, 1.0033766729243974, 1.004317457938491, 0.9902962821172879, 1.0036970498103543, 1.4207407422758753, 0.9360936447457973, 1.0198747623904254, 1.0889750824662074, 1.1516124759011235, 1.3547396244890895, 0.9520268176768387, 1.0839800364550383},
        new double[64] {1.0051501231990063, 1.0028588376627496, 0.9963146828545542, 0.999818336061031, 1.0017585602767844, 1.0030701175896548, 1.001752447613406, 1.0072346693931604, 0.9968358496241568, 1.0004220688924355, 1.0007040117659345, 0.9999469337195395, 0.9990121135105743, 0.9974790129475578, 1.0002849846432877, 1.004020754475833, 0.9961955494668724, 0.9982911615918495, 1.0036087678193102, 0.9997074682182012, 1.001011516051302, 0.9982181210257721, 1.0005164847516024, 1.000756965389453, 0.9987814844721233, 0.996661346693343, 0.9946457379471094, 0.9948532057659111, 1.0057208005024338, 1.0014203302054978, 0.985595686759655, 0.993889650676774, 0.9952248643926122, 0.993209411353407, 1.0001304627334884, 0.9968849415223107, 1.001975406869553, 1.0025548446182986, 0.9804707195234164, 0.9861450738983696, 0.9989266277693497, 0.9983302762863728, 0.9942959331694928, 1.000506342183949, 0.9946527076879843, 1.0198543857307107, 0.9872979488614753, 0.9922549840416395, 0.9978566005043106, 1.0022469493551753, 1.051674909770954, 1.028524584942566, 1.0344686190819739, 0.9988521601766353, 1.002921085747514, 1.001949573560914, 0.9972701840978868, 0.9886754661655237, 1.0041652213668788, 1.286271920439564, 0.9911895129887456, 1.0087809080939796, 0.9972188573082847, 0.9991765513050771},
        new double[64] {0.9998296464048584, 1.0000598479522094, 1.0000871643568798, 1.003564421350188, 1.000354702001295, 0.9998306688347032, 0.9992869383818959, 0.9996853760396814, 0.999977738788176, 1.0011177439688987, 1.0018410802189792, 0.9997521954204459, 1.0011736102874442, 0.9972863912524649, 0.9974206996864218, 0.9955256738611685, 1.0002852187054605, 0.9993828044752368, 1.0006183017048458, 0.995370205788182, 1.0014796043610161, 0.9974887726150895, 1.0002657456609467, 0.9957935410948155, 1.0011912840260158, 1.0045229648912128, 0.9956165685723362, 0.9904698185596705, 0.9919883215889951, 0.9943276922677058, 0.9971249034998579, 0.9993001731862229, 1.0063293147251926, 1.0128518268324058, 0.9926061605746704, 0.9983217648112891, 0.9879661627438039, 1.0020288093848024, 0.9931551256363799, 0.9979927093018017, 1.0056174834411513, 0.9810498829796117, 0.9886547812944888, 0.9909723369240743, 0.9967044992549668, 0.9833316654365217, 1.000917593756518, 0.9958702158552668, 1.0083122374822815, 1.0142451708594515, 1.0023060053972717, 0.9657824258642405, 0.9636592673827327, 1.0089825693344472, 0.9552374039187073, 1.00516442417239, 0.9960157519966564, 1.0406484752020808, 1.0253873370501694, 0.956652735465829, 1.0100270822844726, 0.9743608210686834, 1.2558294973657766, 0.9786605216364087},
    };

}

