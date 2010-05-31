//------------------------------------------------------------------------------
// http://twpol.dyndns.org/projects/fxcop
// License: New BSD License (BSD).
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace IDisposable {
	public class Unclosed : IntrospectionTracer {
		public Unclosed()
			: base("Unclosed", "IDisposable.FxCop", typeof(Unclosed).Assembly) {
		}

		//public override void VisitConstruct(Construct construct) {
		//    var ctor = construct.Constructor as MemberBinding;
		//    if (ctor.BoundMember.DeclaringType.FullName == "Jgr.Gui.AutoCenterWindows") {
		//        Debug.WriteLine("");
		//    }
		//    base.VisitConstruct(construct);
		//}

	}
}
