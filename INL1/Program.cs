internal class Program
{
private static void Main(string[] args){
    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Green;
    string[,] shore = set_shore(); // make shore
    show_shore(shore); // show start

    int[] instructions = {2, 0, 1, 2, 3, 0, 2};
    follow_instructions(shore, instructions);

    Console.ForegroundColor = ConsoleColor.Yellow;
    shore = set_shore(); // reset shore
    int[] ai_instructions = make_instructions(shore);

    show_shore(shore);
    follow_instructions(shore, ai_instructions);
    Console.ForegroundColor = ConsoleColor.White;
}
static void print(object text, bool line = true){ // like python ish
    text = $"{text}";
    if (line)
        Console.WriteLine(text);
    else
        Console.Write(text);
}
static string[,] set_shore(){ // default shore
    string[,] shore = {
        {"" , "" , "" , ""},
        {"farmer", "wolf", "rabbit", "carrot"}
    };
    return shore;
}
static void show_shore(string[,] shore){ // list through the shore and display it
    string msg; // what is written out
    bool last_msg; // if it is the last msg on line
    for (int i = 0; i < shore.GetLength(0); i++){
        for (int j = 0; j < shore.GetLength(1); j++){
            msg = shore[i, j] != "" ? shore[i, j] : "_____";
            last_msg = j == shore.GetLength(1) - 1 ? true : false;

            print($"{msg}\t", last_msg); // print object and if to end the line
        }

        if (i == 0)
            print("\n");
    }
}
static void follow_instructions(string[,] shore, int[] instructions){
    int win_condition; // what shore is not occupied
    if (shore[0, 0] == "")
        win_condition = 1;
    else
        win_condition = 0;

    int idx = 1;
    foreach (int move in instructions){
        string? key = null;
        while (key == null)
            key = Console.ReadLine();
        print($"=< {idx} >========================");
        idx++;

        if (move != 0){ // not only moving object 0 "farmer"
            bool not_swapped = true;
            for (int i = 0; i < shore.GetLength(0); i++){
                if (not_swapped && shore[i, 0] != "" && shore[i, move] != ""){
                    swap(shore, move);
                    not_swapped = false;
                }
            }

            if (not_swapped)
                for (int i = 0; i < shore.GetLength(0); i++)
                    if (shore[i, 0] != "")
                        print($"{shore[i, 0]} has to be on same side\nto move something else");
        }
        swap(shore, 0);

        show_shore(shore);
        check_eaten(shore);

        bool all_moved = true;
        for (int i = 0; i < shore.GetLength(1); i++)
            if (shore[win_condition, i] == "")
                all_moved = false;

        if (all_moved){
            print("all moved");
            return;
        }
    }
    print($"==============================");
}
static void swap(string[,] shore, int column){ // switch row of an object
    string swap;
    swap = shore[0, column];
    shore[0, column] = shore[1, column];
    shore[1, column] = swap;
}
static void check_eaten(string[,] shore){ // shows how and where things went wrong
    for (int i = 0; i < shore.GetLength(0); i++){
        if (shore[i, 0] == ""){ // if object 0 (farmer) is not on that side
            if (shore[i, 2] != "" && shore[i, 3] != ""){ // if object 2 and 3
                print("lost object 3");
                shore[i, 3] = "";
            }
            if (shore[i, 1] != "" && shore[i, 2] != ""){ // if object 1 and 2
                print("lost object 2");
                shore[i, 2] = ""; 
            }
        }
        // for obj 3 to disapear it has to be on same side as obj 2
        // so as long obj 2 gets removed before obj 3 it works well
    }
}
static bool anything_eaten(string[,] shore){ // does not remove just check if invalid move
    for (int i = 0; i < shore.GetLength(0); i++){
        if (shore[i, 0] == ""){ // no object 0 "farmer" present
            if (shore[i, 1] != "" && shore[i, 2] != "")
                return true;
            if (shore[i, 2] != "" && shore[i, 3] != "")
                return true;
        }
    }
    return false;
}
static int[] make_instructions(string[,] shore){ // start and make instructions
    List<int> instructions = new List<int>();

    solve(shore, instructions, -1); // trial and error

    int[] made_instructions = new int[instructions.Count];
    int idx = 0;
    foreach (int move in instructions){ // fix new instruction array
        print(move, false);
        made_instructions[idx] = move;
        idx++;
    }
    print("");
    return made_instructions;
}

static bool solve(string[,] shore, List<int> instructions, int last_move){ // retry until something works
    bool all_moved = true;
    for (int i = 0; i < shore.GetLength(1); i++)
        if (shore[0, i] == "")
            all_moved = false;

    if (all_moved)
        return true;

    for (int move = 0; move < 4; move++){ // try each option
        if (move == last_move)
            continue; // if it was last move continue

        // if (move != 0 && shore[0, 0] != shore[0, move])
        //     continue; // if trying to move object with no farmer

        for (int i = 0; i < shore.GetLength(0); i++)
            if (move != 0 && shore[0, 0] != shore[0, move])
                continue; // if trying to move object with no farmer

        if (move != 0) // move other object than obj 0 "farmer"
            swap(shore, move);
        swap(shore, 0); // move obj 0

        if (anything_eaten(shore)){ // if anything gets eaten reverse the move
            if (move != 0)
                swap(shore, move);
            swap(shore, 0);
        }
        else{ // successfully went through instructions
            instructions.Add(move);
            break;
        }
    }
    solve(shore, instructions, instructions[instructions.Count - 1]); // try again until all move
    return false;
}
}