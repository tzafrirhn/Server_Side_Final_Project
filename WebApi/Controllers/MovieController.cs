﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassLibrary;
using System.Web;
using System.IO;
using WebApi.DTO;

namespace WebApi.Controllers
{
    public class MovieController : ApiController
    {
        // GET api/<controller>
        public List<MovieDto> Get()
        {
            //return new string[] { "value1", "value2" };
            CinemaDB db = new CinemaDB();
            return db.Movies.Select(x => new MovieDto()
            {
                number = x.number,
                name = x.name,
                date = x.date,
                year = x.year,
                genre = x.genre,
                length = x.length,
                movie_img_url = x.movie_img_url,
                seats = x.seats
            }).OrderBy(y => y.date).ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void insertNewMovie()
        {
            Movies movie = new Movies();
            movie.name = HttpContext.Current.Request.Params["name"];
            movie.date = Convert.ToDateTime(HttpContext.Current.Request.Params["date"]);
            movie.year = HttpContext.Current.Request.Params["year"];
            movie.genre = HttpContext.Current.Request.Params["genre"];
            movie.length = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);
            movie.seats = Convert.ToInt32(HttpContext.Current.Request.Params["seats"]);

            HttpPostedFile file = HttpContext.Current.Request.Files["img"];
            string ext = Path.GetExtension(file.FileName);      
            file.SaveAs(HttpContext.Current.Server.MapPath("~") + "/images/" + file.FileName);
            movie.movie_img_url = "/images/" + file.FileName;

            CinemaDB db = new CinemaDB();
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}