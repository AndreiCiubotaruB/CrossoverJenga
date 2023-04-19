using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using UnityEngine;

namespace Crossover.Jenga {
    public class DataManager : BaseManager {
        [Serializable]
        public enum Mastery {
            Glass,
            Wood,
            Stone
        }

        [Serializable]
        public struct StackData {
            public int id;
            public string subject;
            public string grade;
            public Mastery mastery;
            public string domainid;
            public string domain;
            public string cluster;
            public string standardid;
            public string standarddescription;
        }

        [Serializable]
        private struct AllStackData {
            public StackData[] Data;
        }

        [SerializeField] private string _apiEndpoint;

        private AllStackData _data;

        /// <summary>
        /// Memory intensived call as little as possible
        /// </summary>
        public ReadOnlyCollection<StackData> Data => _data.Data.
            OrderBy(x => x.grade).
            ThenBy(x => x.domain).
            ThenBy(x => x.cluster).
            ThenBy(x => x.standardid).
            ToList().AsReadOnly();

        public override void Initialize() {
            var client = new WebClient();
            var get = client.DownloadString(_apiEndpoint);
            _data = JsonHelper.getJsonArray<AllStackData>(get, "Data");
        }

        public override void Uninitialize() {


        }
    } 
}
