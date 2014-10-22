using AnnotationsAnalizer.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace AnnotationsAnalizer
{
    
    [Changes("me", "reason", "01.01.2014")]
    class Analyser
    {
        private AnnotationInfoContext _context;

        public Analyser(AnnotationInfoContext context)
        {
            _context = context;
            _context.Configuration.AutoDetectChangesEnabled = false;
        }

        [Changes("me", "test", "01.01.2014")]
        public void AnalizeAssembly(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return;

            Assembly assembly = Assembly.LoadFile(filename);

            foreach (Type type in assembly.GetTypes())
            {
                //  сохраняем тип
                MemberAnnotationInfo codeUnit = SaveOrGetMember(type.FullName);
                
                //  сохраняем аттрибуты типа
                var attrs = type.GetCustomAttributes(typeof(ChangesAttribute), true);
                foreach (ChangesAttribute attribute in attrs)
                {
                    AnnotationInfo info = AnnotationInfo.FromAttribute(attribute);
                    info.CodeUnt = codeUnit;
                    SaveAnnotation(info);
                }

                //  сохраняем аттрибуты методов
                var methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | 
                                                BindingFlags.NonPublic | BindingFlags.Instance |
                                                BindingFlags.Static);
                foreach(var method in methods)
                {
                    codeUnit = SaveOrGetMember(type.FullName + "." + method.Name);
                    
                    var methodAttrs = method.GetCustomAttributes(typeof(ChangesAttribute), true);
                    foreach (ChangesAttribute attribute in methodAttrs)
                    {
                        AnnotationInfo info = AnnotationInfo.FromAttribute(attribute);
                        info.CodeUnt = codeUnit;
                        SaveAnnotation(info);
                    }
                }
            }

        }

        [Changes("asdf", "test2", "03.01.1992")]
        private MemberAnnotationInfo SaveOrGetMember(string memberName)
        {
            MemberAnnotationInfo codeUnit = _context.Members.SingleOrDefault(m => m.Name == memberName);

            if (codeUnit == null)
            {
                codeUnit = new MemberAnnotationInfo() { Name = memberName };

                _context.Members.Add(codeUnit);
                try
                {
                    _context.SaveChanges();
                }
                catch { }
            }

            return codeUnit;
        }

        [Changes("asdf", "test2", "03.01.1992")]
        private void SaveAnnotation(AnnotationInfo annotaition)
        {
            if (_context.Annotations.FirstOrDefault(a => a.Author == annotaition.Author &&
                                                    a.Reason == annotaition.Reason &&
                                                    a.Date == annotaition.Date &&
                                                    a.CodeUnt.Name == annotaition.CodeUnt.Name) != null)
                return;

            _context.Annotations.Add(annotaition);
            try
            {
                _context.SaveChanges();
            }
            catch { }
        }

        [Changes("a", "staticmethod", "03.01.1992")]
        private static void StaticTEstMethod(string t)
        {
            Console.Out.Write("lskdjf");
        }

    }
}
