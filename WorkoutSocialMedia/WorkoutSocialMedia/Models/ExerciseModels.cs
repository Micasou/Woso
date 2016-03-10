using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutSocialMedia.Enums;
namespace WorkoutSocialMedia.Models
{
    public class Exercise
    {
        public virtual int ExerciseId { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual String Target { get; set; }
    }
    public class Workout
    {
        public virtual int WorkoutId { get; set; }
        public virtual int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual String Reps { get; set; }
    }
    
}