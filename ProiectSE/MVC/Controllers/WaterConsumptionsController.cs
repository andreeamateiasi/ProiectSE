﻿using MVC.Models;
using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class WaterConsumptionsController : Controller
    {
        // GET: WaterConsumptions
        public List<WaterConsumption> GetWaterConsumptions()
        {
            RestClient<WaterConsumption> rc = new RestClient<WaterConsumption>();
            rc.WebServiceUrl = "http://localhost:55428/api/waterconsumptions/";
            var waterConsumptionList = rc.GetAsync();
            return waterConsumptionList;
        }

        public WaterConsumption GetWaterConsumptionById(int id)
        {
            RestClient<WaterConsumption> rc = new RestClient<WaterConsumption>();
            rc.WebServiceUrl = "http://localhost:55428/api/waterconsumptions/";
            var waterConsumption = rc.GetByIdAsync(id);
            return waterConsumption;
        }

        public bool PostWaterConsumption(WaterConsumption wc)
        {
            RestClient<WaterConsumption> rc = new RestClient<WaterConsumption>();
            rc.WebServiceUrl = "http://localhost:55428/api/waterconsumptions/";
            bool response = rc.PostAsync(wc);
            return response;
        }

        public bool PutWaterConsumption(int id, WaterConsumption wc)
        {
            RestClient<WaterConsumption> rc = new RestClient<WaterConsumption>();
            rc.WebServiceUrl = "http://localhost:55428/api/waterconsumptions/";
            bool response = rc.PutAsync(id, wc);
            return response;
        }

        //public bool DeleteWaterConsumption(int id, WaterConsumption wc)
        //{
        //    RestClient<WaterConsumption> rc = new RestClient<WaterConsumption>();
        //    rc.WebServiceUrl = "http://localhost:55428/api/waterconsumptions/";
        //    bool response = rc.DeleteAsync(id, wc);
        //    return response;
        //}

        public ActionResult List()
        {
            //WaterConsumption wCons = new WaterConsumption();
            //wCons.WaterConsumptionId = 1;
            //wCons.ApartmentId = 1;
            //wCons.PricePerUnit = 20;
            //wCons.Consumption = 10;
            //wCons.AmountOfMoneyOwed = 200;
            //List<WaterConsumption> wConsList = new List<WaterConsumption>();
            //wConsList.Add(wCons);
            UsersController uc = new UsersController();
            string email = Request.Cookies["UserCookie"].Value;
            List<User> user = new List<User>();
            user = uc.GetUserByEmail(email);
            if (user[0].Role == "user")
            {
                List<WaterConsumption> wConsList = new List<WaterConsumption>();
                foreach (var apartment in user[0].Apartments)
                {
                    foreach (var wc in apartment.WaterConsumptions)
                    {
                        wConsList.Add(wc);
                    }
                }
                return View(wConsList);
            }
            else
            {
                List<WaterConsumption> wConsList = GetWaterConsumptions();

                return View(wConsList);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}