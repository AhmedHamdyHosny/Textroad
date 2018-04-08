using Classes.Utilities;
using GenericApiController.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Models
{
    public abstract class GenericModel<TModel> where TModel : class
    {
        public string ControllerRoute { get; set; }
        public string ApiServerUrl { get; set; }
        public GenericModel(string ApiUrl, string ApiRoute)
        {
            ApiServerUrl = ApiUrl;
            ControllerRoute = ApiRoute;
        }
        public virtual List<TModel> Get()
        {
            string url = ApiServerUrl + ControllerRoute;
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var task = request.Execute<List<TModel>>();
            task.Wait();
            return task.Result;
        }
        public virtual TModel Get(object id)
        {
            string url = ApiServerUrl + ControllerRoute + id;
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Get);
            var task = request.Execute<TModel>();
            task.Wait();
            return task.Result;
        }
        public virtual List<TModel> Get(GenericDataFormat requestBody)
        {
            string url = ApiServerUrl + ControllerRoute + "get";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<List<TModel>>();
            task.Wait();
            return task.Result;
        }
        public virtual PaginationResult<T> GetView<T>(GenericDataFormat requestBody)
        {
            string url = ApiServerUrl + ControllerRoute + "getView";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<PaginationResult<T>>();
            task.Wait();
            return task.Result;
        }
        public virtual PaginationResult<T> GetGridView<T>(GenericDataFormat requestBody)
        {
            string url = ApiServerUrl + ControllerRoute + "getGridView";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<PaginationResult<T>>();
            task.Wait();
            return task.Result;
        }
        public virtual TModel Insert(TModel obj)
        {
            string url = ApiServerUrl + ControllerRoute;
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<TModel>();
            task.Wait();
            return task.Result;
        }
        public virtual TModel Update(TModel obj, object id)
        {
            string url = ApiServerUrl + ControllerRoute + id;
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Put) { RequestBody = new StringContent(JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<TModel>();
            task.Wait();
            return task.Result;
        }
        public virtual List<TModel> Update(List<UpdateItemFormat<TModel>> newItems)
        {
            string url = ApiServerUrl + ControllerRoute + "put";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(newItems), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<List<TModel>>();
            task.Wait();
            return task.Result;
        }
        public virtual TModel UpdateWithReference(TModel obj)
        {
            string url = ApiServerUrl + ControllerRoute + "PutWithReference";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<TModel>();
            task.Wait();
            return task.Result;
        }
        
        public virtual bool Delete(object id)
        {
            string url = ApiServerUrl + ControllerRoute + id;
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Delete);
            var task = request.Execute<String>();
            task.Wait();
            if (task.Result.ToString().Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Delete(object[] ids)
        {
            string url = ApiServerUrl + ControllerRoute + "delete";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<String>();
            task.Wait();
            if (task.Result != null && task.Result.ToString().Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Deactive(object id)
        {
            string url = ApiServerUrl + ControllerRoute + "deactive/" + id;
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Delete);
            var task = request.Execute<String>();
            task.Wait();
            if (task.Result.ToString().Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Deactive(object[] ids)
        {
            string url = ApiServerUrl + ControllerRoute + "deactive";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<String>();
            task.Wait();
            if (task.Result.ToString().Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Import(TModel[] objs)
        {
            string url = ApiServerUrl + ControllerRoute + "import";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(objs), System.Text.Encoding.UTF8, "application/json") };
            var task = request.Execute<string>();
            task.Wait();

            if (task.Result != null && task.Result.ToString().Contains("Success"))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public virtual bool Import(HttpPostedFileBase file)
        {
            string url = ApiServerUrl + ControllerRoute + "import/file";
            var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(file.InputStream), file.FileName, file.FileName);
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = formData };
            var task = request.ExcecuteImport(file);
            task.Wait();
            if (task.Result != null && task.Result.ToString().Contains("Success"))
            {
                return true;
            }
            else
            {
                return false;
            }
            //string result = await response.Content.ReadAsStringAsync();


        }
        public virtual byte[] Export(GenericDataFormat requestBody = null)
        {
            string url = ApiServerUrl + ControllerRoute + "export";
            MyHttpRequestMessage request = new MyHttpRequestMessage(url, HttpMethod.Post) { RequestBody = new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json") };
            var task = request.ExcecuteExport();
            task.Wait();
            return task.Result;


        }
        public virtual List<TModel> GetAsDDLst(string includeProperties, string sortByProperty, List<GenericDataFormat.FilterItems> filters = null, GenericDataFormat.SortType sortType = GenericDataFormat.SortType.Asc, bool GetByView = false)
        {
            var sorts = new List<GenericDataFormat.SortItems>();
            sorts.Add(new GenericDataFormat.SortItems() { Property = sortByProperty, SortType = sortType });
            //create request body parameters
            GenericDataFormat requestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = includeProperties }, Sorts = sorts };
            requestBody.Filters = filters;
            if (!GetByView)
            {
                var lst = this.Get(requestBody);
                return lst;
            }
            else
            {
                var lst = this.GetView<TModel>(requestBody).PageItems;
                return lst;
            }
        }
    }
}