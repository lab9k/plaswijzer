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
        private string cs;


        public QueryManager()
        {
            cs = "Data Source=toilets.db";
        }

        /// <summary>
        /// Method for getting the nearest free toilets. First using private method to get all the free toilets out of database and then using KDtree algorithm to select the nearest
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<IToilet> GetNearestFreeToilets(float lon, float lat, int count)
        {
            List<IToilet> freeToilets = GetAllFreeToilets();

            KdTree<float, Toilet> tree = new KdTree<float, Toilet>(2, new FloatMath());
            foreach(var toilet in freeToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, (Toilet) toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<IToilet> nearestFree = new List<IToilet>();
            foreach(KdTreeNode<float,Toilet> t in nearest)
            {
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
        public List<IToilet> GetNearestToilets(float lon, float lat, int count)
        {

            List<IToilet> tToilets = GetAllToilets();

            KdTree<float, Toilet> tree = new KdTree<float, Toilet>(2, new FloatMath());
            foreach (var toilet in tToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, (Toilet) toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<IToilet> nearestToilet = new List<IToilet>();
            foreach (KdTreeNode<float, Toilet> t in nearest)
            {
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
        public List<IToilet> GetNearestGehandToilets(float lon, float lat, int count)
        {
            List<IToilet> gehandToilets = GetAllGehandToilets();

            KdTree<float, GehandToilet> tree = new KdTree<float, GehandToilet>(2, new FloatMath());
            foreach (var toilet in gehandToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, (GehandToilet) toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<IToilet> nearestGehand = new List<IToilet>();
            foreach (KdTreeNode<float, GehandToilet> t in nearest)
            {
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
        public List<IToilet> GetNearestDogToilets(float lon, float lat, int count)
        {
            List<IToilet> dogToilets = GetAllDogToilets();

            KdTree<float, DogToilet> tree = new KdTree<float, DogToilet>(2, new FloatMath());
            foreach (var toilet in dogToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, (DogToilet) toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<IToilet> nearestDog = new List<IToilet>();
            foreach (KdTreeNode<float, DogToilet> t in nearest)
            {
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
        public List<IToilet> GetNearestUriToilets(float lon, float lat, int count)
        {
            List<IToilet> uriToilets = GetAllUrinoirs();

            KdTree<float, Urinoir> tree = new KdTree<float, Urinoir>(2, new FloatMath());
            foreach (var toilet in uriToilets)
            {
                tree.Add(new float[] { toilet.Lon, toilet.Lat }, (Urinoir) toilet);
            }

            var nearest = tree.GetNearestNeighbours(new float[] { lon, lat }, count);
            List<IToilet> nearestUri = new List<IToilet>();
            foreach (KdTreeNode<float, Urinoir> t in nearest)
            {
                nearestUri.Add(t.Value);
            }
            return nearestUri;
        }


        /// <summary>
        /// Method for getting all free toilets from sql database
        /// </summary>
        /// <returns></returns>
        private List<IToilet> GetAllFreeToilets()
        {
            List<IToilet> freeToilets = new List<IToilet>();
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
        private List<IToilet> GetAllToilets()
        {
            List<IToilet> tToilets = new List<IToilet>();
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
        private List<IToilet> GetAllGehandToilets()
        {
            List<IToilet> gehandToilets = new List<IToilet>();
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
        private List<IToilet> GetAllDogToilets()
        {
            List<IToilet> dogToilets = new List<IToilet>();
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
        private List<IToilet> GetAllUrinoirs()
        {
            List<IToilet> uriToilets = new List<IToilet>();
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
