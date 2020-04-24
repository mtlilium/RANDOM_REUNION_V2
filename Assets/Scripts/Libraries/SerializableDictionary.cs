using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Serialize {

    /// <summary>
    /// テーブルの管理クラス
    /// </summary>
    [System.Serializable]
    public class TableBase<TKey, TValue, Type> where Type : KeyAndValue<TKey, TValue> {
        [SerializeField]
        private List<Type> list = null;
        private Dictionary<TKey, TValue> table;


        public Dictionary<TKey, TValue> GetTable() {
            if (table == null) {
                table = ConvertListToDictionary(list);
            }
            return table;
        }

        /// <summary>
        /// Editor Only
        /// </summary>
        public List<Type> GetList() {
            return list;
        }

        static Dictionary<TKey, TValue> ConvertListToDictionary(List<Type> list) {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
            foreach (KeyAndValue<TKey, TValue> pair in list) {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }
    }

    /// <summary>
    /// シリアル化できる、KeyValuePair
    /// </summary>
    [System.Serializable]
    public class KeyAndValue<TKey, TValue> {
        public TKey Key;
        public TValue Value;

        public KeyAndValue(TKey key, TValue value) {
            Key = key;
            Value = value;
        }
        public KeyAndValue(KeyValuePair<TKey, TValue> pair) {
            Key = pair.Key;
            Value = pair.Value;
        }


    }
}
//https://qiita.com/k_yanase/items/fb64ccfe1c14567a907d から
/*
利用するときは
1, Serialize.KeyAndValueを継承して、KeyとValueの型を指定したクラスを作る
(KeyとValueを引数に持つコンストラクタを作らないといけない)
2, Serialize.TableBaseを継承して、KeyとValueに加えて1で作成したクラスを指定したクラスを作る
3, 2で作ったクラスを利用したいMonoBehaviourのスクリプトに変数定義
*/