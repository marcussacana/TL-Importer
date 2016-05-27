using System;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Globalization;
using System.Linq;

namespace TL_Importer {
    class HighLevelCodeProcessator {
        public HighLevelCodeProcessator(string Code) {
            System.IO.StringReader Sr = new System.IO.StringReader(Code);
            string[] Lines = new string[0];
            while (Sr.Peek() != -1) {
                string[] tmp = new string[Lines.Length + 1];
                Lines.CopyTo(tmp, 0);
                tmp[Lines.Length] = Sr.ReadLine();
                Lines = tmp;
            }
            Engine = InitializeEngine(Lines);
        }
        Assembly Engine;
        public static void Crash() {
            Crash();
        }

        /// <summary>
        /// Call a Function with arguments and return the result
        /// </summary>
        /// <param name="ClassName">Class o the target function</param>
        /// <param name="FunctionName">Target function name</param>
        /// <param name="Arguments">Function parameters</param>
        /// <returns></returns>
        public object Call(string ClassName, string FunctionName, params object[] Arguments) { 
            object ret = exec(Arguments, ClassName, FunctionName, Engine);
            return ret;
        }
        
        private object instance = null;
        private object exec(object[] Args, string Class, string Function, Assembly assembly) {
            Type fooType = assembly.GetType(Class);
            if (instance == null)
                instance = assembly.CreateInstance(Class);
            MethodInfo printMethod = fooType.GetMethod(Function);
            return printMethod.Invoke(instance, BindingFlags.InvokeMethod, null, Args, CultureInfo.CurrentCulture);
        }
        private Assembly InitializeEngine(string[] lines) {
            CodeDomProvider cpd = new CSharpCodeProvider();
            var cp = new CompilerParameters();
            string sourceCode = string.Empty;
            foreach (string line in lines) {
                if (line.StartsWith("using ") && line.EndsWith(";"))
                    cp.ReferencedAssemblies.Add(line.Substring(6, line.Length - 7) + ".dll");
                sourceCode += line.Replace("\t", "") + '\n';
            }
            cp.GenerateExecutable = false;
            CompilerResults cr = cpd.CompileAssemblyFromSource(cp, sourceCode);
            return cr.CompiledAssembly;
        }
    }
}
