using KdTree;
using KdTree.Math;
using Microsoft.Data.Sqlite;
using Plaswijzer.Model;
using Plaswijzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Data
{
    public class QueryManager: IQueryManager
    {
        private string cs = "Data Source=toilets.db";

        /// <summary>
        /// Method for getting the nearest free toilets. First using private method to get all the free toilets out of database and then using KDtree algorithm to select the nearest
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Toilet> GetNearestFreeToilets(float lon, float lat, int count)
        {
            List<Toilet> freeToilets = GetAllFreeToilets();

            KdTree<float, Toilet> tree = new KdTree<float, Toilet>(2, new FloatMath());
            foreach(var toilet in freeToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<Toilet> nearestFree = new List<Toilet>();
            foreach(KdTreeNode<float,Toilet> t in nearest)
            {
                Console.WriteLine("nearest toilet " + t.Value.ToString());
                nearestFree.Add(t.Value);
            }
            return nearestFree;
        }

        /// <summary>
        /// Method for getting the nearest toilets. First using private method to get all toilets out of database and then using KDtree algorithm to select the nearest
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Toilet> GetNearestToilets(float lon, float lat, int count)
        {
            List<Toilet> tToilets = GetAllToilets();

            KdTree<float, Toilet> tree = new KdTree<float, Toilet>(2, new FloatMath());
            foreach (var toilet in tToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<Toilet> nearestToilet = new List<Toilet>();
            foreach (KdTreeNode<float, Toilet> t in nearest)
            {
                Console.WriteLine("nearest tttoilet " + t.Value.ToString());
                nearestToilet.Add(t.Value);
            }
            return nearestToilet;
        }

       

        /// <summary>
        /// Method for getting the nearest weelchair toilets. First using private method to get all the Gehandtoilets out of database and then using KDtree algorithm to select the nearest
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<GehandToilet> GetNearestGehandToilets(float lon, float lat, int count)
        {
            List<GehandToilet> gehandToilets = GetAllGehandToilets();

            KdTree<float, GehandToilet> tree = new KdTree<float, GehandToilet>(2, new FloatMath());
            foreach (var toilet in gehandToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<GehandToilet> nearestGehand = new List<GehandToilet>();
            foreach (KdTreeNode<float, GehandToilet> t in nearest)
            {
                Console.WriteLine("gehand toilet " + t.Value.ToString());
                nearestGehand.Add(t.Value);
            }
            return nearestGehand;
        }

        /// <summary>
        /// Method for getting the nearest dogtoilets. First using private method to get all the dogtoilets out of database and then using KDtree algorithm to select the nearest
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<DogToilet> GetNearestDogToilets(float lon, float lat, int count)
        {
            List<DogToilet> dogToilets = GetAllDogToilets();

            KdTree<float, DogToilet> tree = new KdTree<float, DogToilet>(2, new FloatMath());
            foreach (var toilet in dogToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<DogToilet> nearestDog = new List<DogToilet>();
            foreach (KdTreeNode<float, DogToilet> t in nearest)
            {
                Console.WriteLine("Dog toilet " + t.Value.ToString());
                nearestDog.Add(t.Value);
            }
            return nearestDog;
        }


        /// <summary>
        /// Method for getting the nearest urinoirs. First using private method to get all the urinoirs out of database and then using KDtree algorithm to select the nearest
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Urinoir> GetNearestUriToilets(float lon, float lat, int count)
        {
            List<Urinoir> uriToilets = GetAllUrinoirs();

            KdTree<float, Urinoir> tree = new KdTree<float, Urinoir>(2, new FloatMath());
            foreach (var toilet in uriToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<Urinoir> nearestUri = new List<Urinoir>();
            foreach (KdTreeNode<float, Urinoir> t in nearest)
            {
                Console.WriteLine("Uri toilet " + t.Value.ToString());
                nearestUri.Add(t.Value);
            }
            return nearestUri;
        }


        /// <summary>
        /// Method for getting all free toilets from sql database
        /// </summary>
        /// <returns></returns>
        private List<Toilet> GetAllFreeToilets()
        {
            List<Toilet> freeToilets = new List<Toilet>();
            using(SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
                string query = "SELECT * FROM Toilet WHERE Gratis=1";
                using(SqliteCommand cmd = new SqliteCommand(query, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Toilet toilet = new Toilet();
                            toilet.ID = rdr.GetString(0);
                            toilet.Situering = rdr.GetString(1);
                            toilet.Open7op7 = rdr.GetInt32(2);
                            toilet.Openuren = rdr.GetString(3);
                            toilet.Gratis = rdr.GetInt32(4);
                            toilet.Type_locat = rdr.GetString(5);
                            toilet.Lon = rdr.GetFloat(6);
                            toilet.Lat = rdr.GetFloat(7);
                            toilet.Type = rdr.GetString(8);

                            Console.WriteLine("Free toilet: " + toilet.ToString());

                            freeToilets.Add(toilet);
                        }
                        rdr.Close();
                    }
                }
            }
            return freeToilets;
        }



        /// <summary>
        /// Method for getting all toilets from sql database
        /// </summary>
        /// <returns></returns>
        private List<Toilet> GetAllToilets()
        {
            List<Toilet> tToilets = new List<Toilet>();
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
                string query = "SELECT * FROM Toilet";
                using (SqliteCommand cmd = new SqliteCommand(query, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Toilet toilet = new Toilet
                            {
                                ID = rdr.GetString(0),
                                Situering = rdr.GetString(1),
                                Open7op7 = rdr.GetInt32(2),
                                Openuren = rdr.GetString(3),
                                Gratis = rdr.GetInt32(4),
                                Type_locat = rdr.GetString(5),
                                Lon = rdr.GetFloat(6),
                                Lat = rdr.GetFloat(7),
                                Type = rdr.GetString(8)
                            };

                            Console.WriteLine("toilet: " + toilet.ToString());

                            tToilets.Add(toilet);
                        }
                        rdr.Close();
                    }
                }
            }
            return tToilets;
        }


        /// <summary>
        /// Method for getting all weelchair toilets from sql database
        /// </summary>
        /// <returns></returns>
        private List<GehandToilet> GetAllGehandToilets()
        {
            List<GehandToilet> gehandToilets = new List<GehandToilet>();
            using (SqliteConnection con = new SqliteConnection(cs))
            {

                con.Open();
                string query = "SELECT * FROM GehandToilet";
                using (SqliteCommand cmd = new SqliteCommand(query, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            GehandToilet toilet = new GehandToilet();
                            toilet.ID = rdr.GetString(0);
                            toilet.Situering = rdr.GetString(1);
                            toilet.Open7op7 = rdr.GetInt32(2);
                            toilet.Openuren = rdr.GetString(3);
                            toilet.Gratis = rdr.GetInt32(4);
                            toilet.Type_locat = rdr.GetString(5);
                            toilet.Lon = rdr.GetFloat(6);
                            toilet.Lat = rdr.GetFloat(7);

                            Console.WriteLine("Gehand toilet: " + toilet.ToString());

                            gehandToilets.Add(toilet);
                        }
                        rdr.Close();
                    }
                }
            }
            return gehandToilets;
        }


        /// <summary>
        /// Method for getting all dogtoilets from sql database
        /// </summary>
        /// <returns></returns>
        private List<DogToilet> GetAllDogToilets()
        {
            List<DogToilet> dogToilets = new List<DogToilet>();
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
                string query = "SELECT * FROM DogToilet";
                using (SqliteCommand cmd = new SqliteCommand(query, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DogToilet toilet = new DogToilet();
                            toilet.ID = rdr.GetString(0);
                            toilet.Situering = rdr.GetString(1);
                            toilet.Lon = rdr.GetFloat(2);
                            toilet.Lat = rdr.GetFloat(3);

                            Console.WriteLine("dog toilet: " + toilet.ToString());

                            dogToilets.Add(toilet);
                        }
                        rdr.Close();
                    }
                }
            }
            return dogToilets;
        }

        /// <summary>
        /// Method for getting all urinoirs from sql database
        /// </summary>
        /// <returns></returns>
        private List<Urinoir> GetAllUrinoirs()
        {
            List<Urinoir> uriToilets = new List<Urinoir>();
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
                string query = "SELECT * FROM Urinoir";
                using (SqliteCommand cmd = new SqliteCommand(query, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Urinoir toilet = new Urinoir();
                            toilet.ID = rdr.GetString(0);
                            toilet.Situering = rdr.GetString(1);
                            toilet.Open7op7 = rdr.GetInt32(2);
                            toilet.Openuren = rdr.GetString(3);
                            toilet.Gratis = rdr.GetInt32(4);
                            toilet.Type_locat = rdr.GetString(5);
                            toilet.Lon = rdr.GetFloat(6);
                            toilet.Lat = rdr.GetFloat(7);

                            Console.WriteLine("Urinoir: " + toilet.ToString());

                            uriToilets.Add(toilet);
                        }
                        rdr.Close();
                    }
                }
            }
            return uriToilets;
        }


    }
}
