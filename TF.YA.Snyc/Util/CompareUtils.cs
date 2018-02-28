using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
namespace TF.YA.Sync
{
    class CompareUtils<T> where T : SyncClass
    {
        private List<T> _NewList = new List<T>();
        private List<T> _UpdateLists = new List<T>();
        private List<T> _RemoveList = new List<T>();
      
        public void Clear()
        {
            _NewList.Clear();
            _UpdateLists.Clear();
            _RemoveList.Clear();
           
        }
        public List<T> NewList
        {
            get { return _NewList; }
        }
        public List<T> RemoveList
        {
            get { return _RemoveList; }
        }
        public List<T> UpdateList
        {
            get { return _UpdateLists; }
        }


        public bool Compare(List<T> Source, List<T> Dest)
        {
            Clear();

            //对实体列表进行排序
            Source.Sort((p1, p2) => { return p1.Key.CompareTo(p2.Key); });

            Dest.Sort((p1, p2) => { return p1.Key.CompareTo(p2.Key); });


            //对比是否相同
            if (Source.Count == Dest.Count)
            {
                Boolean bSame = true;
                for (int i = 0; i < Source.Count; i++)
                {
                    if (Source[i].Md5 != Dest[i].Md5)
                    {
                        bSame = false;
                        break;
                    }
                }
                if (bSame)
                {
                    return true;
                }

            }


            //查找需要删除或更新的实体
            foreach (var o in Source)
            {
                var dest = Dest.Find(p => p.Key == o.Key);
                if (dest != null)
                {
                    if (dest.Md5 != o.Md5)
                    {
                        UpdateList.Add(dest);
                    }
                }
                else
                {
                    RemoveList.Add(o);
                }

            }

            //查找需要添加的实体
            foreach (var o in Dest)
            {
                if (!Source.Exists(p => p.Key == o.Key))
                {
                    NewList.Add(o);
                }
            }

            return false;
        }
    }
}
