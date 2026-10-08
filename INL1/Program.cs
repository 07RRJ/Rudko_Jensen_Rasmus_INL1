internal class Program
{
    private static void Main(string[] args){
        Console.Clear();
        Console.WriteLine("Hello, World!");

        string[,] shore = {
            {"" , "" , "" , ""},
            {"farmer", "wolf", "rabbit", "carrot"}
        };

        string[] instructions = {
            "02",
            "0",
            "01",
            "02",
            "03",
            "0",
            "02",
        };

        show_shore(shore);
        follow_instructions(shore, instructions);
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
    static void follow_instructions(string[,] shore, string[] instructions){
        int idx = 1;
        string swap;
        foreach (string move in instructions){
            print($"=<{idx}>==========================", true);
            idx += 1;
            if (move[0] != '0')
                print("\nthe boat can not move without the farmer\n", true);
            else{
                foreach (char colum in move){
                    int column = colum - '0';
                    swap = shore[0, column];
                    shore[0, column] = shore[1, column];
                    shore[1, column] = swap;
                }
                show_shore(shore);
                check_rules(shore);
            }
        }
    }
    static void check_rules(string[,] shore){
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
}