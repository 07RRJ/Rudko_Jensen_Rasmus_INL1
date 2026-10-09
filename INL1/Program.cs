internal class Program
{
    private static void Main(string[] args){
        Console.Clear();

        string[,] shore = set_shore();
        int[] instructions = {2, 0, 1, 2, 3, 0, 2}; // have tested and this works

        Console.ForegroundColor = ConsoleColor.Green;
        show_shore(shore);
        follow_instructions(shore, instructions);

        shore = set_shore();
        int[] ai_instructions = make_instructions(shore);

        Console.ForegroundColor = ConsoleColor.Yellow;
        follow_instructions(shore, ai_instructions);
        Console.ForegroundColor = ConsoleColor.White;
    }
    static void print(object text, bool line = false){
        text = $"{text}";
        if (line)
            Console.WriteLine(text);
        else
            Console.Write(text);
    }
    static string[,] set_shore(){
        string[,] shore = {
            {"" , "" , "" , ""},
            {"farmer", "wolf", "rabbit", "carrot"}
        };
        return shore;
    }
    static void show_shore(string[,] shore){
        string msg;
        bool last_msg;
        for (int i = 0; i < shore.GetLength(0); i++){
            for (int j = 0; j < shore.GetLength(1); j++){
                msg = shore[i, j] != "" ? shore[i, j] : "_____";
                last_msg = j == shore.GetLength(1) - 1 ? true : false; 
                print($"{msg}\t", last_msg);
            }
            if (i == 0)
                print("\n", true);
        }
    }
    static void follow_instructions(string[,] shore, int[] instructions){
        int idx = 1;
        foreach (int move in instructions){
            string? key = null;
            while (key == null)
                key = Console.ReadLine();
            print($"=< {idx} >========================", true);
            idx += 1;
            if (move != 0){
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
                            print($"{shore[i, 0]} has to be on same side\nto move something else", true);
            }
            swap(shore, 0);
            show_shore(shore);
            check_eaten(shore);
            bool all_moved = true;
            for (int i = 0; i < shore.GetLength(1); i++)
                if (shore[0, i] == "")
                    all_moved = false;
            if (all_moved){
                print("all moved", true);
                return;
            }
        }
    }
    static void swap(string[,] shore, int column){
        string swap;
        swap = shore[0, column];
        shore[0, column] = shore[1, column];
        shore[1, column] = swap;
    }
    static void check_eaten(string[,] shore){
        bool object_2_lost = false, object_3_lost = false;
        for (int i = 0; i < shore.GetLength(0); i++){
            if (shore[i, 0] == ""){
                if (shore[i, 1] != "" && shore[i, 2] != "")
                    object_2_lost = true;
                if (shore[i, 2] != "" && shore[i, 3] != "")
                    object_3_lost = true;
            }
            for (int l = 0; l < shore.GetLength(0); l++){
                if (object_2_lost)
                    shore[l, 2] = "";
                if (object_3_lost)
                    shore[l, 3] = "";
            }
        }
        if (object_2_lost)
            print("lost object 2", true);
        if (object_3_lost)
            print("lost object 3", true);
    }
    static int[] make_instructions(string[,] shore){
        List<int> instructions = new List<int>();
        Random random = new Random();
        int idx = 1, rng = 0;
        bool not_solved = true;
        while (not_solved){
            while (rng == instructions[idx])
                rng = random.Next(0, 4);
            instructions[idx] = rng;
            if (test_ai(instructions, shore))
                idx += 1;
        }
        foreach (int i in instructions)
            print(i);
        print("\n", true);

        int[] made_instructions = new int[instructions.Count];
        for (int i = 0; i < instructions.Count; i++)
            made_instructions[i] = instructions[i];

        return made_instructions;
    }
    static bool test_ai(List<int> instructions, string[,] shore){
        int idx = 1;
        foreach (int move in instructions){
            string? key = null;
            while (key == null)
                key = Console.ReadLine();
            print($"=< {idx} >========================", true);
            idx += 1;
            if (move != 0){
                bool not_swapped = true;
                for (int i = 0; i < shore.GetLength(0); i++){
                    if (not_swapped && shore[i, 0] != "" && shore[i, move] != ""){
                        swap(shore, move);
                        not_swapped = false;
                    }
                }
                if (not_swapped)
                    for (int i = 0; i < shore.GetLength(0); i++)
                        if (shore[i, 0] != ""){
                            print($"{shore[i, 0]} has to be on same side\nto move something else", true);
                            return false;
                        }
            }
            swap(shore, 0);
        }
    return true;
    }
}