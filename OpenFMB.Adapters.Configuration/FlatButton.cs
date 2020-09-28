using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public class FlatButton : Button
    {        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Cursor = Cursors.Hand;            
            base.OnMouseMove(e);
        }
    }
}
