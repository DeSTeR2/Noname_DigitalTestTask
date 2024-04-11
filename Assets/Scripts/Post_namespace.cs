using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Only for json parsing
namespace Post {
    [System.Serializable]
    class Posts {
        public Mods[] mods;
        public string[] categories;
    }
    [System.Serializable]
    class Mods {
        public string category;
        public string preview_path;
        public string file_path;
        public string title;
        public string description;
    }

}