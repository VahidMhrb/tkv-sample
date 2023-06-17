using Application.Common.Attributes;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;
using System.Reflection;

namespace Application.Common.Models.Response.Base
{
    public class ResponseBase
    {
        [JsonIgnore]
        public int Status { get; set; }
        [JsonIgnore]
        public int ResponseCode { get; set; }
        [JsonIgnore]
        public object[]? ActionMessageParams { get; set; }
        public string? ActionCode
        {
            get
            {
                ActionCodeDic.TryGetValue(ResponseCode, out string? desc);
                return desc;
            }
        }
        public string? ActionMessage
        {
            get
            {
                ActionMessageDic.TryGetValue(ResponseCode, out string? desc);
                if (desc != null && ActionMessageParams != null && ActionMessageParams.Any())
                    return string.Format(desc, ActionMessageParams);
                return desc;
            }
        }
        public IEnumerable<string>? ErrorMessages { get; set; }

        [JsonIgnore]
        public HttpStatusCode? HttpStatusCode
        {
            get
            {
                HttpStatusCodeDic.TryGetValue(ResponseCode, out HttpStatusCode? httpStatusCode);
                return httpStatusCode;
            }
        }

        private static readonly Dictionary<int, string?> ActionCodeDic = new();
        private static readonly Dictionary<int, string?> ActionMessageDic = new();
        private static readonly Dictionary<int, HttpStatusCode?> HttpStatusCodeDic = new();

        static ResponseBase()
        {
            var type = typeof(Constants.Response);
            var baseType = type.BaseType;

            var descData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, DescriptionAttribute?>(p, p.GetCustomAttribute<DescriptionAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, string>((int?)t.Item1.GetValue(null), t.Item2?.Description ?? "")).ToList();


            var httpStatusData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, HttpStatusAttribute?>(p, p.GetCustomAttribute<HttpStatusAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, HttpStatusCode?>((int?)t.Item1.GetValue(null), t.Item2?.HttpStatus)).ToList();

            var actionCodeData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, ActionCodeAttribute?>(p, p.GetCustomAttribute<ActionCodeAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, string?>((int?)t.Item1.GetValue(null), t.Item2?.ActionCode)).ToList();

            if (baseType != null)
            {
                descData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, DescriptionAttribute?>(p, p.GetCustomAttribute<DescriptionAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, string>((int?)t.Item1.GetValue(null), t.Item2?.Description ?? "")).ToList()
                );


                httpStatusData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, HttpStatusAttribute?>(p, p.GetCustomAttribute<HttpStatusAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, HttpStatusCode?>((int?)t.Item1.GetValue(null), t.Item2?.HttpStatus)).ToList()
                );

                actionCodeData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, ActionCodeAttribute?>(p, p.GetCustomAttribute<ActionCodeAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, string?>((int?)t.Item1.GetValue(null), t.Item2?.ActionCode)).ToList()
                );
            }


            descData.ForEach(i => ActionMessageDic.Add(i.Item1 ?? 0, i.Item2));
            httpStatusData.ForEach(i => HttpStatusCodeDic.Add(i.Item1 ?? 0, i.Item2));
            actionCodeData.ForEach(i => ActionCodeDic.Add(i.Item1 ?? 0, i.Item2));

        }
    }

    public class ResponseBase<TClass>
    {
        [JsonIgnore]
        public int Status { get; set; }

        [JsonIgnore]
        public int ResponseCode { get; set; }

        [JsonIgnore]
        public object[]? ActionMessageParams { get; set; }

        public string? ActionCode
        {
            get
            {
                ActionCodeDic.TryGetValue(ResponseCode, out string? desc);
                return desc;
            }
        }
        public string? ActionMessage
        {
            get
            {
                ActionMessageDic.TryGetValue(ResponseCode, out string? desc);
                if (desc != null && ActionMessageParams != null && ActionMessageParams.Any())
                    return string.Format(desc, ActionMessageParams);
                return desc;
            }
        }
        public IEnumerable<string>? ErrorMessages { get; set; }

        public TClass? Data { get; set; }

        [JsonIgnore]
        public HttpStatusCode? HttpStatusCode
        {
            get
            {
                HttpStatusCodeDic.TryGetValue(ResponseCode, out HttpStatusCode? httpStatusCode);
                return httpStatusCode;
            }
        }

        private static readonly Dictionary<int, string?> ActionCodeDic = new();
        private static readonly Dictionary<int, string?> ActionMessageDic = new();
        private static readonly Dictionary<int, HttpStatusCode?> HttpStatusCodeDic = new();

        static ResponseBase()
        {
            var type = typeof(Constants.Response);
            var baseType = type.BaseType;

            var descData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, DescriptionAttribute?>(p, p.GetCustomAttribute<DescriptionAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, string>((int?)t.Item1.GetValue(null), t.Item2?.Description ?? "")).ToList();


            var httpStatusData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, HttpStatusAttribute?>(p, p.GetCustomAttribute<HttpStatusAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, HttpStatusCode?>((int?)t.Item1.GetValue(null), t.Item2?.HttpStatus)).ToList();

            var actionCodeData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, ActionCodeAttribute?>(p, p.GetCustomAttribute<ActionCodeAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, string?>((int?)t.Item1.GetValue(null), t.Item2?.ActionCode)).ToList();

            if (baseType != null)
            {
                descData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, DescriptionAttribute?>(p, p.GetCustomAttribute<DescriptionAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, string>((int?)t.Item1.GetValue(null), t.Item2?.Description ?? "")).ToList()
                );


                httpStatusData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, HttpStatusAttribute?>(p, p.GetCustomAttribute<HttpStatusAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, HttpStatusCode?>((int?)t.Item1.GetValue(null), t.Item2?.HttpStatus)).ToList()
                );

                actionCodeData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, ActionCodeAttribute?>(p, p.GetCustomAttribute<ActionCodeAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, string?>((int?)t.Item1.GetValue(null), t.Item2?.ActionCode)).ToList()
                );
            }


            descData.ForEach(i => ActionMessageDic.Add(i.Item1 ?? 0, i.Item2));
            httpStatusData.ForEach(i => HttpStatusCodeDic.Add(i.Item1 ?? 0, i.Item2));
            actionCodeData.ForEach(i => ActionCodeDic.Add(i.Item1 ?? 0, i.Item2));

        }
    }

    public class ListResponseBase<TClass>
    {
        [JsonIgnore]
        public int Status { get; set; }
        [JsonIgnore]
        public int ResponseCode { get; set; }
        [JsonIgnore]
        public object[]? ActionMessageParams { get; set; }

        public string? ActionCode
        {
            get
            {
                ActionCodeDic.TryGetValue(ResponseCode, out string? desc);
                return desc;
            }
        }
        public string? ActionMessage
        {
            get
            {
                ActionMessageDic.TryGetValue(ResponseCode, out string? desc);
                if (desc != null && ActionMessageParams != null && ActionMessageParams.Any())
                    return string.Format(desc, ActionMessageParams);
                return desc;
            }
        }
        public IEnumerable<string>? ErrorMessages { get; set; }

        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages
        {
            get
            {
                if (PageSize > 0 && TotalCount > PageSize)
                    return int.Parse(Math.Ceiling((decimal)TotalCount / PageSize).ToString());
                return 1;
            }
        }
        public string? NextPage { get; set; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public List<TClass>? Data { get; set; }

        [JsonIgnore]
        public HttpStatusCode? HttpStatusCode
        {
            get
            {
                HttpStatusCodeDic.TryGetValue(ResponseCode, out HttpStatusCode? httpStatusCode);
                return httpStatusCode;
            }
        }

        private static readonly Dictionary<int, string?> ActionCodeDic = new();
        private static readonly Dictionary<int, string?> ActionMessageDic = new();
        private static readonly Dictionary<int, HttpStatusCode?> HttpStatusCodeDic = new();

        static ListResponseBase()
        {
            var type = typeof(Constants.Response);
            var baseType = type.BaseType;

            var descData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, DescriptionAttribute?>(p, p.GetCustomAttribute<DescriptionAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, string>((int?)t.Item1.GetValue(null), t.Item2?.Description ?? "")).ToList();


            var httpStatusData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, HttpStatusAttribute?>(p, p.GetCustomAttribute<HttpStatusAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, HttpStatusCode?>((int?)t.Item1.GetValue(null), t.Item2?.HttpStatus)).ToList();

            var actionCodeData = type.GetFields()
                .Select(p => new Tuple<FieldInfo, ActionCodeAttribute?>(p, p.GetCustomAttribute<ActionCodeAttribute>()))
                .Where(t => t.Item2 != null)
                .Select(t => new Tuple<int?, string?>((int?)t.Item1.GetValue(null), t.Item2?.ActionCode)).ToList();

            if (baseType != null)
            {
                descData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, DescriptionAttribute?>(p, p.GetCustomAttribute<DescriptionAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, string>((int?)t.Item1.GetValue(null), t.Item2?.Description ?? "")).ToList()
                );


                httpStatusData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, HttpStatusAttribute?>(p, p.GetCustomAttribute<HttpStatusAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, HttpStatusCode?>((int?)t.Item1.GetValue(null), t.Item2?.HttpStatus)).ToList()
                );

                actionCodeData.AddRange(
                    baseType.GetFields()
                    .Select(p => new Tuple<FieldInfo, ActionCodeAttribute?>(p, p.GetCustomAttribute<ActionCodeAttribute>()))
                    .Where(t => t.Item2 != null)
                    .Select(t => new Tuple<int?, string?>((int?)t.Item1.GetValue(null), t.Item2?.ActionCode)).ToList()
                );
            }


            descData.ForEach(i => ActionMessageDic.Add(i.Item1 ?? 0, i.Item2));
            httpStatusData.ForEach(i => HttpStatusCodeDic.Add(i.Item1 ?? 0, i.Item2));
            actionCodeData.ForEach(i => ActionCodeDic.Add(i.Item1 ?? 0, i.Item2));
        }
    }

}
