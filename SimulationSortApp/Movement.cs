using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationSortApp
{
    class Movement
    {
        public enum LoaiDiChuyen
        {
            //Định nghĩa các cách di chuyển
            DI_LEN_DI_XUONG, //Nút 1 đi lên, nút 2 đi xuống
            QUA_PHAI_QUA_TRAI, //Nút 1 qua phải, nút 2 qua trái
            DI_XUONG_DI_LEN, DUNG //Nút 1 đi xuống, nút 2 đi lên
        }
        //vị trí lúc di chuyển
        public class Status
        {
            public LoaiDiChuyen Type { get; set; }
            public int Vt1 { get; set; }
            public int Vt2 { get; set; }
        }
    }
}