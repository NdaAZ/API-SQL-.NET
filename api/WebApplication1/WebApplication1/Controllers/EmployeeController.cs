using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Asset_id, Asset_Name, Asset_Category, Model, Department, Asset_Floor, X_axis, Y_axis,
                            Warranty_Exp,PhotoFileName
                            from
                            dbo.AssetRegister
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssetTrackingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"
                           insert into dbo.AssetRegister
                           (Asset_id, Asset_Name, Asset_Category, Model, Department, Asset_Floor, X_axis, Y_axis,Warranty_Exp,PhotoFileName)
                    values (@Asset_id, @Asset_Name, @Asset_Category, @Model, @Department, @Asset_Floor, @X_axis, @Y_axis, @Warranty_Exp,@PhotoFileName)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssetTrackingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Asset_id", emp.Asset_id);
                    myCommand.Parameters.AddWithValue("@Asset_Name", emp.Asset_Name);
                    myCommand.Parameters.AddWithValue("@Asset_Category", emp.Asset_Category);
                    myCommand.Parameters.AddWithValue("@Model", emp.Model);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@Asset_Floor", emp.Asset_Floor);
                    myCommand.Parameters.AddWithValue("@X_axis", emp.X_axis);
                    myCommand.Parameters.AddWithValue("@Y_axis", emp.Y_axis);
                    myCommand.Parameters.AddWithValue("@Warranty_Exp", emp.Warranty_Exp);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
                           update dbo.AssetRegister
                           set Asset_Name= @Asset_Name,
                            Asset_Category=@Asset_Category,
                            Model=@Model,
                            Department=@Department,
                            Asset_Floor=@Asset_Floor,
                            X_axis=@X_axis,
                            Y_axis=@Y_axis,
                            Warranty_Exp=@Warranty_Exp,
                            PhotoFileName=@PhotoFileName
                            where Asset_id=@Asset_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssetTrackingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Asset_id", emp.Asset_id);
                    myCommand.Parameters.AddWithValue("@Asset_Name", emp.Asset_Name);
                    myCommand.Parameters.AddWithValue("@Asset_Category", emp.Asset_Category);
                    myCommand.Parameters.AddWithValue("@Model", emp.Model);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@Asset_Floor", emp.Asset_Floor);
                    myCommand.Parameters.AddWithValue("@X_axis", emp.X_axis);
                    myCommand.Parameters.AddWithValue("@Y_axis", emp.Y_axis);
                    myCommand.Parameters.AddWithValue("@Warranty_Exp", emp.Warranty_Exp);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.AssetRegister
                            where Asset_id=@Asset_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssetTrackingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Asset_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

    }
}
