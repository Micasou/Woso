using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutSocialMedia.Enums
{
    public static class ExerciseEnums
    {

        public static WorkoutLevel workoutLevel(String input)
        {
            if(input == "Begginer")
                return WorkoutLevel.Begginer;
            else if (input == "Intermediate")
                return WorkoutLevel.Intermediate;
            return WorkoutLevel.Expert;
        }
        public static Force force(String input)
        {
            if (input == "Pull")
                return Force.Pull;
            else if (input == "Push")
                return Force.Push;
            return Force.NA;
        }
        public static Muscle muscle(String input)
        {
            if (input == "Abdominals")
                return Muscle.Abdominals;
            else if (input == "Lats")
                return Muscle.Lats;
            else if (input == "Lower Back")
                return Muscle.LowerBack;
            else if (input == "Adductors")
                return Muscle.Adductors;
            else if (input == "Middle Back")
                return Muscle.MiddleBack;
            else if (input == "Biceps")
                return Muscle.Biceps;
            else if (input == "Neck")
                return Muscle.Neck;
            else if (input == "Shoulders")
                return Muscle.Shoulders;
            else if (input == "Chest")
                return Muscle.Chest;
            else if (input == "Quadriceps")
                return Muscle.Quadriceps;
            else if (input == "Calves")
                return Muscle.Calves;

            else if (input == "Traps")
                return Muscle.Traps;
            else if (input == "Glutes")
                return Muscle.Glutes;
            else if (input == "Triceps")
                return Muscle.Triceps;
            return Muscle.Forearms;
        }
    }
    public enum WorkoutLevel
    {
        Begginer,
        Intermediate,
        Expert
    }
    public enum Force
    {
        NA,
        Push,
        Pull
    }
    public enum Muscle
    {
        Abdominals,
        Lats,
        Abductors,
        LowerBack,
        Adductors,
        MiddleBack,
        Biceps,
        Neck,
        Calves,
        Quadriceps,
        Chest,
        Shoulders,
        Forearms,
        Traps,
        Glutes,
        Triceps,
        Hamstrings
    }
}