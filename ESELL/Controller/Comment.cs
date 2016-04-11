using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Controller
{
    public class Comment
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public bool Addcomment(string desc, int news_id)
        {
            ESHOP_NEWS_COMMENT cm = new ESHOP_NEWS_COMMENT();
            cm.NEWS_ID = news_id;
            cm.COMMENT_CONTENT = desc;
            cm.COMMENT_PUBLISHDATE = DateTime.Now;
            db.ESHOP_NEWS_COMMENTs.InsertOnSubmit(cm);
            db.SubmitChanges();
            return true;
        }
        public bool Addcomment(string desc, int news_id, int commentId)
        {
            ESHOP_NEWS_COMMENT cm = new ESHOP_NEWS_COMMENT();
            cm.NEWS_ID = news_id;
            cm.COMMENT_CONTENT = desc;
            cm.COMMENT_FIELD3 = commentId.ToString();
            cm.COMMENT_PUBLISHDATE = DateTime.Now;
            db.ESHOP_NEWS_COMMENTs.InsertOnSubmit(cm);
            db.SubmitChanges();
            return true;
        }
        public IQueryable Load_Reply(object commentId)
        {
            try
            {
                var show = (from b in db.ESHOP_NEWS_COMMENTs
                            where b.COMMENT_FIELD3 == commentId.ToString()
                            select b).OrderByDescending(n => n.COMMENT_PUBLISHDATE);
                return show;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AddQuestion(string name, string email, string title, string desc, int news_id, int _rating)
        {
            ESHOP_NEWS_COMMENT cm = new ESHOP_NEWS_COMMENT();
            cm.NEWS_ID = news_id;
            cm.COMMENT_CONTENT = desc;
            cm.COMMENT_NAME = name;
            cm.COMMENT_EMAIL = email;
            cm.COMMENT_FIELD1 = title;
            cm.COMMENT_FIELD2 = _rating.ToString();
            cm.COMMENT_STATUS = 0;
            cm.COMMENT_PUBLISHDATE = DateTime.Now;
            db.ESHOP_NEWS_COMMENTs.InsertOnSubmit(cm);
            db.SubmitChanges();
            return true;
        }
        public List<ESHOP_NEWS_COMMENT> Load_comment(string _url)
        {
            try
            {
                var show = (from a in db.ESHOP_NEWs
                            join b in db.ESHOP_NEWS_COMMENTs on a.NEWS_ID equals b.NEWS_ID
                            where a.NEWS_SEO_URL == _url && b.COMMENT_STATUS == 1
                            select b).OrderByDescending(n => n.COMMENT_PUBLISHDATE).ToList();
                return show;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<ESHOP_NEWS_COMMENT> Load_comment(string _url, string _rating)
        {
            try
            {
                var show = (from a in db.ESHOP_NEWs
                            join b in db.ESHOP_NEWS_COMMENTs on a.NEWS_ID equals b.NEWS_ID
                            where a.NEWS_SEO_URL == _url && b.COMMENT_FIELD2 == _rating && b.COMMENT_STATUS == 1
                            select b).OrderByDescending(n => n.COMMENT_PUBLISHDATE).ToList();
                return show;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
