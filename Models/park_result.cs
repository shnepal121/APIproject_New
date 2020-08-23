using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// Model = [Root, Root, Root]
//Root =>  {html_attributions, list[],status,..}
namespace APIproject.Models
{
    public class Root
    {
        [Key]
        public int Id { get; set; }
        public List<string> html_attributions { get; set; }
        public string next_page_token { get; set; }        
        public List<ResultsItem> results { get; set; }        
        public string status { get; set; }
    }


    public class ResultsItem
    {
        
        public string business_status { get; set; }       
        public string formatted_address { get; set; }       
        public Geometry geometry { get; set; }        
        public string icon { get; set; }       
        public string id { get; set; }       
        public string name { get; set; }      
        public Opening_hours opening_hours { get; set; }
       
        public List<PhotosItem> photos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string place_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Plus_code plus_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reference { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> types { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int user_ratings_total { get; set; }
    }


    public class Location
        {            
            public double lat { get; set; }          
            public double lng { get; set; }
        }

        public class Northeast
        {          
           
            public double lat { get; set; }          
            public double lng { get; set; }
        }

        public class Southwest
        {          
            public double lat { get; set; }           
            public double lng { get; set; }
        }

        public class Viewport
        {
         
            public Northeast northeast { get; set; }         
            public Southwest southwest { get; set; }
        }

        public class Geometry
        {
          
            public Location location { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Viewport viewport { get; set; }
        }

        public class Opening_hours
        {
            /// <summary>
            /// 
            /// </summary>
            public string open_now { get; set; }
        }

        public class PhotosItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int height { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> html_attributions { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string photo_reference { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int width { get; set; }
        }

        public class Plus_code
        {
            /// <summary>
            /// 
            /// </summary>
            public string compound_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string global_code { get; set; }
        }       





    
}

