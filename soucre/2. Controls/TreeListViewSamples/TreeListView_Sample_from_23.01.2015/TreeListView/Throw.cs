using System;
using System.Diagnostics;

/*namespace TLVDemo
{
   public class Throw
   {
      #region IfTrue Methods

      public static void IfTrue(bool condition, string errorMessage)
      {
         Throw.IfTrue(condition, new Exception(errorMessage));
      }

      public static void IfTrue(bool condition,
             string fmtSpec,
             params object[] args)
      {
         Debug.Assert(fmtSpec != null);
 
         string errorText = string.Format(args == null ? fmtSpec 
                   : string.Format(fmtSpec, args));

         Throw.IfTrue(condition, new Exception(errorText);
      }

      public static void IfTrue(bool condition, Exception ex)
      {
         Debug.Assert(ex != null);
         if (condition)
         {
            throw ex;
         }
      }

      #endregion

      #region IfFalse Methods

      public static void IfFalse(bool condition, string errorMessage)
      {
         Throw.IfTrue(!condition, errorMessage);
      }

      public static void IfFalse(bool condition, Exception ex)
      {
         Throw.IfTrue(!condition, ex);
      }

      public static void IfFalse(bool condition,
             string fmtSpec,
             params object[] args)
      {
         Throw.IfTrue(!condition, fmtSpec, args);
      }

      #endregion

      #region IfNull Methods

      public static void IfNull<T>(T objRef, string errorMessage)
      {
         Throw.IfTrue(objRef == null, errorMessage);
      }

      private static void IfNull<T>(T objRef,
              string fmtSpec,
              object[] args)
      {
         Throw.IfTrue(objRef == null, fmtSpec, args);
      }

      public static void IfNull<T>(T objRef, Exception ex)
      {
         Throw.IfTrue(objRef == null, ex);
      }

      #endregion

      #region IfNotNull Methods

      public static void IfNotNull<T>(T objRef, string errorMessage)
      {
         Throw.IfTrue(objRef != null, errorMessage);
      }

      public static void IfNotNull<T>(T objRef,
             string fmtSpec,
             params object[] args)
      {
         Throw.IfTrue(objRef != null, fmtSpec, args);
      }

      public static void IfNotNull<T>(T objRef, Exception ex)
      {
         Throw.IfTrue(objRef != null, ex);
      }

      #endregion
   }
}*/

public class Throw
{
   #region IfTrue Methods

   public static void IfTrue(bool condition, string errorMessage)
   {
      Throw.IfTrue(condition, new Exception(errorMessage));
   }

   public static void IfTrue(bool condition,
          string fmtSpec,
          params object[] args)
      {
         Debug.Assert(fmtSpec != null);
 
         string errorText = string.Format(args == null ? fmtSpec 
                   : string.Format(fmtSpec, args));

         Throw.IfTrue(condition, new Exception(errorText));
      }

   public static void IfTrue(bool condition, Exception ex)
   {
      Debug.Assert(ex != null);
      if (condition)
      {
         throw ex;
      }
   }

   #endregion

   #region IfFalse Methods

   public static void IfFalse(bool condition, string errorMessage)
   {
      Throw.IfTrue(!condition, errorMessage);
   }

   public static void IfFalse(bool condition, Exception ex)
   {
      Throw.IfTrue(!condition, ex);
   }

   public static void IfFalse(bool condition,
          string fmtSpec,
          params object[] args)
   {
      Throw.IfTrue(!condition, fmtSpec, args);
   }

   #endregion

   #region IfNull Methods

   public static void IfNull<T>(T objRef, string errorMessage)
   {
      Throw.IfTrue(objRef == null, errorMessage);
   }

   private static void IfNull<T>(T objRef,
           string fmtSpec,
           object[] args)
   {
      Throw.IfTrue(objRef == null, fmtSpec, args);
   }

   public static void IfNull<T>(T objRef, Exception ex)
   {
      Throw.IfTrue(objRef == null, ex);
   }

   #endregion

   #region IfNotNull Methods

   public static void IfNotNull<T>(T objRef, string errorMessage)
   {
      Throw.IfTrue(objRef != null, errorMessage);
   }

   public static void IfNotNull<T>(T objRef,
          string fmtSpec,
          params object[] args)
   {
      Throw.IfTrue(objRef != null, fmtSpec, args);
   }

   public static void IfNotNull<T>(T objRef, Exception ex)
   {
      Throw.IfTrue(objRef != null, ex);
   }

   #endregion
}