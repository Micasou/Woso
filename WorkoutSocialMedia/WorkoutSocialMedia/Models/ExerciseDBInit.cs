using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.IO;
using System.Reflection;
using System.Text;
using WorkoutSocialMedia.Enums;

namespace WorkoutSocialMedia.Models
{
    public class ExerciseDBInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<WorkoutSocialMedia.Models.ExerciseDBContext>
    {
        protected override void Seed(ExerciseDBContext context)
        {
            StringBuilder temp = new StringBuilder();
            temp.Append("http://www.bodybuilding.com/exercises/list/index/selected/");
            for (int i = 0; i < 26; i++)
            {
                if ((char)(((int)'a') + i) == 'x') //special case, no file path with x
                    i++;
                temp.Append((char)(((int)'a') + i));//cast char to ascii table val, then increment up to z.
                List<Exercise> exer = this.getExercises(temp.ToString()); //sends the path from a-z
                foreach (Exercise e in exer)
                    context.Exercises.Add(e);
                temp.Length--; //unpoint to last index for creating new filepath.
            }
            base.Seed(context);
        }
        public List<Exercise> getExercises(String path)
        {
            List<Exercise> exercises = new List<Exercise>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = new HtmlDocument();
            document = web.Load(path);
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//div[@class='exerciseName']").ToArray();
            foreach (HtmlNode i in nodes)
            {
                Exercise temp = new Exercise();
                String[] str = this.parseNameAndTarget(i.InnerText);
                temp.Name = str[0];
                temp.Target = str[1];
                String workoutPath = parsePath(i.OuterHtml);
                document = web.Load(workoutPath);//get the link within the node, goes to exercise
                HtmlNode Description = document.DocumentNode.SelectSingleNode("//div[@class='guideContent']");
                HtmlNode exerData = document.DocumentNode.SelectSingleNode("//div[@id='exerciseDetails']"); //get the node with exercise details
                temp.Description = Description.InnerText; 
                exercises.Add(temp);
            }
            return exercises;
        }
      
        private String parsePath(String s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            sb.Replace("<div class=\"exerciseName\">", "");
            sb.Replace(" ", "");
            sb.Replace("\n<h3><ahref=\'", "");
            s = sb.ToString();
            sb.Clear();
            for (int i = 0; i < s.Length; i++)
                if (s[i + 1] != '>') //we found the link.
                    sb.Append(s[i]);
                else
                    break;
            return sb.ToString();
        }
        private String[] parseNameAndTarget(String s)
        {
            StringBuilder sb = new StringBuilder();
            String[] temp = new String[2];
            sb.Append(s);
            sb.Replace(" Muscle Targeted: ", "::"); //Crop out unwanted information.
            String str = sb.ToString();
            for (int i = 0; i < sb.Length; i++ )
            {
                if(str[i] == ':' && str[i+1] == ':')
                {
                    sb.Remove(i, sb.Length - i); //remove last part of string
                    temp[0] = sb.ToString(); //we have the name
                    sb.Clear(); //clear it out
                    sb.Append(str); //replace it
                    sb.Replace(temp[0],""); //replace the workout with nothing
                    sb.Replace("::", ""); //replace the workout with nothing
                    temp[1] = sb.ToString(); //append
                    break;
                }
            }
            return temp;
        }
    }
}