using System;

namespace V1RU3_Outbreak
{
    public class Pipe
    {
        //define global varaibles
        public int rotation { get; set; }
        public int type { get; set; }

        //constructor
        public Pipe(int rotation, int type)
        {
            this.rotation = rotation;
            this.type = type;
        }
    }
}
