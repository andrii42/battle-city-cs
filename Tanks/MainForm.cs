using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tanks
{
    public partial class MainForm : Form
    {
        // Creating our View component:
        ViewControl view;
        
        // Creating a model:
        CModel model;  

        public MainForm()
        {            
            //
            model = new CModel();
            //
            view = new ViewControl(model);
            //
            // Adding View to our controls list:
            this.Controls.Add(view);
            //
            InitializeComponent();
        }
    }
}
