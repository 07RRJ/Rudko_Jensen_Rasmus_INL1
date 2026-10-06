internal class Program
{
    private static void Main(string[] args){
        cls();
        Console.WriteLine("Hello, World!");

        // while (true){
        //     print("> ");
        //     print(Console.ReadLine(), true);
        // }

        string[,] shore = {
            {"" , "" , "" , ""},
            {"farmer", "wolf", "rabbit", "carrot"}
        };

        show_list(shore);

        // int[,] instructions = {
            
        // };

        // for (int i = 0; i < instructions.Length; i++){
        //     print(instructions[i]);
        // }
    }
    static void cls(){
        Console.Clear();
    }
    static void print(object text, bool line = false){
        text = $"{text}";
        if (line)
            Console.WriteLine(text);
        else
            Console.Write(text);
    }
    static void show_list(string[,] list){
        for (int i = 0; i < list.GetLength(0); i++)
            for (int j = 0; j < list.GetLength(1); j++)
                print($"{list[i, j]}\t");
            print("\n", true);
    }
}