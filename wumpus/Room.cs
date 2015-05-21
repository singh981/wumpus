/* 
 * Room Class Version 1.0 22/2/2015
 * Description: Room class containing three main attributes.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Room
    {
        private string roomNumber;
        private string adjacentRooms;
        private string roomDescription;

        public Room() { }


        public Room(string rn, string ar, string rd) 
        {
            this.roomNumber = rn;
            this.adjacentRooms = ar;
            this.roomDescription = rd;

        }

        public string RoomNumber 
        {
            get { return this.roomNumber; }
            set { this.roomNumber = value; }
        }

        public string AdjacentRooms
        {
            get { return this.adjacentRooms; }
            set { this.adjacentRooms = value; }
        }

        public string RoomDescription 
        {
            get { return this.roomDescription; }
            set { this.roomDescription = value; }
        }




    }
}
