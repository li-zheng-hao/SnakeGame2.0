using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AccountRT
    {
        public int id;
        
        public string username;

        public string password;

        public int goldcount;


        public AccountRT(Account ac)
        {
            this.id = ac.id;
            this.username = ac.username;
            this.password = ac.password;
            this.goldcount = ac.goldcount;
        }

    }

}

