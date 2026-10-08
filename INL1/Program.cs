internal class Program
{
    private static void Main(string[] args){
        Console.Clear();
        Console.WriteLine("Hello, World!");
        Console.ResetColor(); 
        // Console.BackgroundColor = ConsoleColor.Blue;
        // Console.ForegroundColor = ConsoleColor.Blue;

        string[,] shore = {
            {"" , "" , "" , ""},
            {"farmer", "wolf", "rabbit", "carrot"}
        };
        int[] instructions = {2, 0, 1, 2, 3, 0, 2}; // have been tested and they work

        show_shore(shore);
        follow_instructions(shore, instructions);

        // reset_shore(shore);
        // int[] ai_instructions = new int[10];
        // make_instructions(ai_instructions);
        
        // follow_instructions(shore, ai_instructions);
    }
    static void print(object text, bool line = false){
        text = $"{text}";
        if (line)
            Console.WriteLine(text);
        else
            Console.Write(text);
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
            print($"=< {idx} >========================", true);
            idx += 1;
            if (move != 0){
                bool not_swapped = true;
                for (int i = 0; i < shore.GetLength(0); i++){
                    if (not_swapped && shore[i, 0] == "farmer" && shore[i, move] != ""){
                        swap(shore, move);
                        not_swapped = false;
                    }
                }
                if (not_swapped)
                    print("farmer has to be on same side to move an object or animal", true);
            }
            swap(shore, 0);
            show_shore(shore);
            check_eaten(shore);
        }
    }
    static void swap(string[,] shore, int column){
        string swap;
        swap = shore[0, column];
        shore[0, column] = shore[1, column];
        shore[1, column] = swap;
    }
    static void check_eaten(string[,] shore){
        int dead_rabbit = 0, eaten_carrot = 0;
        for (int i = 0; i < shore.GetLength(0); i++){
            if (shore[i, 0] != "farmer")
                for (int j = 0; j < shore.GetLength(1); j++){
                    if (shore[i, j] == "wolf" || shore[i, j] == "rabbit")
                        dead_rabbit += 1;
                    if (shore[i, j] == "rabbit" || shore[i, j] == "carrot")
                        eaten_carrot += 1;
                }
            for (int l = 0; l < shore.GetLength(0); l++){
                if (dead_rabbit == 2)
                    shore[l, 2] = "";
                if (eaten_carrot == 2)
                    shore[l, 3] = "";
            }
        }
        if (eaten_carrot == 2)
            print("the carrot got eaten", true);
        if (dead_rabbit == 2)
            print("the rabbit got eaten", true);
    }
    static void reset_shore(string[,] shore){
        string[,] original_shore = {
            {"" , "" , "" , ""},
            {"farmer", "wolf", "rabbit", "carrot"}
        };
        shore = original_shore;
    }
    static void make_instructions(int[] instructions){
        Random random = new Random();
        int idx = 1, rng = 0;
        bool not_solved = true;
        while (not_solved && idx < instructions.Length){
            while (rng == instructions[idx - 1]){
                rng = random.Next(0, 4);
            }
            instructions[idx] = rng;
            print(rng);
            idx += 1;
        }
        print("\n", true);
    }
}