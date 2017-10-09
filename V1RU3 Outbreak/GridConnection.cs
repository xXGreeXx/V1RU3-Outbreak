using System;

namespace V1RU3_Outbreak
{
    public class GridConnection
    {
        //define global variables
        public int baseIndex { get; set; }
        public int targetIndex { get; set; }
        public int[] baseOffset { get; set; }
        public int[] targetOffset { get; set; }

        //constructor
        public GridConnection(int baseIndex, int targetIndex, int[] baseOffset, int[] targetOffset)
        {
            this.baseIndex = baseIndex;
            this.targetIndex = targetIndex;
            this.baseOffset = baseOffset;
            this.targetOffset = targetOffset;
        }
    }
}
