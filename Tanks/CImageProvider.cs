using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CImageProvider
    {
        
        //Enemy1:
        public static List<Image> enemy1_img_down = new List<Image> { Properties.Resources.enemy1_down_1, Properties.Resources.enemy1_down_2 };

        public static List<Image> enemy1_img_up = new List<Image> { Properties.Resources.enemy1_up_1, Properties.Resources.enemy1_up_2 };

        public static List<Image> enemy1_img_left = new List<Image> { Properties.Resources.enemy1_left_1, Properties.Resources.enemy1_left_2 };

        public static List<Image> enemy1_img_right = new List<Image> { Properties.Resources.enemy1_right_1, Properties.Resources.enemy1_right_2 };

        public static List<Image> enemy1_img_down_bonus = new List<Image> { Properties.Resources.enemy1_bonus_down_1, Properties.Resources.enemy1_bonus_down_2 };

        public static List<Image> enemy1_img_up_bonus = new List<Image> { Properties.Resources.enemy1_bonus_up_1, Properties.Resources.enemy1_bonus_up_2 };

        public static List<Image> enemy1_img_left_bonus = new List<Image> { Properties.Resources.enemy1_bonus_left_1, Properties.Resources.enemy1_bonus_left_2 };

        public static List<Image> enemy1_img_right_bonus = new List<Image> { Properties.Resources.enemy1_bonus_right_1, Properties.Resources.enemy1_bonus_right_2 };
        
        //Enemy2:
        public static List<Image> enemy2_img_down = new List<Image> { Properties.Resources.enemy2_down_1, Properties.Resources.enemy2_down_2 };

        public static List<Image> enemy2_img_up = new List<Image> { Properties.Resources.enemy2_up_1, Properties.Resources.enemy2_up_2 };

        public static List<Image> enemy2_img_left = new List<Image> { Properties.Resources.enemy2_left_1, Properties.Resources.enemy2_left_2 };

        public static List<Image> enemy2_img_right = new List<Image> { Properties.Resources.enemy2_right_1, Properties.Resources.enemy2_right_2 };

        public static List<Image> enemy2_img_down_bonus = new List<Image> { Properties.Resources.enemy2_bonus_down_1, Properties.Resources.enemy2_bonus_down_2 };

        public static List<Image> enemy2_img_up_bonus = new List<Image> { Properties.Resources.enemy2_bonus_up_1, Properties.Resources.enemy2_bonus_up_2 };

        public static List<Image> enemy2_img_left_bonus = new List<Image> { Properties.Resources.enemy2_bonus_left_1, Properties.Resources.enemy2_bonus_left_2 };

        public static List<Image> enemy2_img_right_bonus = new List<Image> { Properties.Resources.enemy2_bonus_right_1, Properties.Resources.enemy2_bonus_right_2 };


        //Enemy3:
        public static List<Image> enemy3_img_down = new List<Image> { Properties.Resources.enemy3_down_1, Properties.Resources.enemy3_down_2 };

        public static List<Image> enemy3_img_up = new List<Image> { Properties.Resources.enemy3_up_1, Properties.Resources.enemy3_up_2 };

        public static List<Image> enemy3_img_left = new List<Image> { Properties.Resources.enemy3_left_1, Properties.Resources.enemy3_left_2 };

        public static List<Image> enemy3_img_right = new List<Image> { Properties.Resources.enemy3_right_1, Properties.Resources.enemy3_right_2 };

        public static List<Image> enemy3_img_down_bonus = new List<Image> { Properties.Resources.enemy3_bonus_down_1, Properties.Resources.enemy3_bonus_down_2 };

        public static List<Image> enemy3_img_up_bonus = new List<Image> { Properties.Resources.enemy3_bonus_up_1, Properties.Resources.enemy3_bonus_up_2 };

        public static List<Image> enemy3_img_left_bonus = new List<Image> { Properties.Resources.enemy3_bonus_left_1, Properties.Resources.enemy3_bonus_left_2 };

        public static List<Image> enemy3_img_right_bonus = new List<Image> { Properties.Resources.enemy3_bonus_right_1, Properties.Resources.enemy3_bonus_right_2 };


        //Enemy4:
        public static List<Image>[] enemy4_down = { 
                                             /* 1 Life: WHITE images*/          new List<Image> { Properties.Resources.enemy4_down_1, Properties.Resources.enemy4_down_2 }, 
                                             
                                             /* 2 Life: GREEN + YELLOW images*/ new List<Image> { Properties.Resources.enemy4_green_down_1, Properties.Resources.enemy4_green_down_2, Properties.Resources.enemy4_yellow_down_1,Properties.Resources.enemy4_yellow_down_2  },
                                             
                                             /* 3 Life: WHITE + YELLOW images*/ new List<Image> { Properties.Resources.enemy4_down_1, Properties.Resources.enemy4_down_2, Properties.Resources.enemy4_yellow_down_1,Properties.Resources.enemy4_yellow_down_2, }, 
                                             
                                             /* 4 Life: WHITE + GREEN images*/  new List<Image> { Properties.Resources.enemy4_down_1, Properties.Resources.enemy4_down_2, Properties.Resources.enemy4_green_down_1, Properties.Resources.enemy4_green_down_2 } 
                                            };

        public static List<Image>[] enemy4_up = { 
                                             /* 1 Life: WHITE images*/          new List<Image> { Properties.Resources.enemy4_up_1, Properties.Resources.enemy4_up_2 }, 
                                             
                                             /* 2 Life: GREEN + YELLOW images*/ new List<Image> { Properties.Resources.enemy4_green_up_1, Properties.Resources.enemy4_green_up_2, Properties.Resources.enemy4_yellow_up_1,Properties.Resources.enemy4_yellow_up_2  },
                                             
                                             /* 3 Life: WHITE + YELLOW images*/ new List<Image> { Properties.Resources.enemy4_up_1, Properties.Resources.enemy4_up_2, Properties.Resources.enemy4_yellow_up_1,Properties.Resources.enemy4_yellow_up_2, }, 
                                             
                                             /* 4 Life: WHITE + GREEN images*/  new List<Image> { Properties.Resources.enemy4_up_1, Properties.Resources.enemy4_up_2, Properties.Resources.enemy4_green_up_1, Properties.Resources.enemy4_green_up_2 } 
                                            };

        public static List<Image>[] enemy4_left = { 
                                             /* 1 Life: WHITE images*/          new List<Image> { Properties.Resources.enemy4_left_1, Properties.Resources.enemy4_left_2 }, 
                                             
                                             /* 2 Life: GREEN + YELLOW images*/ new List<Image> { Properties.Resources.enemy4_green_left_1, Properties.Resources.enemy4_green_left_2, Properties.Resources.enemy4_yellow_left_1,Properties.Resources.enemy4_yellow_left_2  },
                                             
                                             /* 3 Life: WHITE + YELLOW images*/ new List<Image> { Properties.Resources.enemy4_left_1, Properties.Resources.enemy4_left_2, Properties.Resources.enemy4_yellow_left_1,Properties.Resources.enemy4_yellow_left_2, }, 
                                             
                                             /* 4 Life: WHITE + GREEN images*/  new List<Image> { Properties.Resources.enemy4_left_1, Properties.Resources.enemy4_left_2, Properties.Resources.enemy4_green_left_1, Properties.Resources.enemy4_green_left_2 } 
                                            };

        public static List<Image>[] enemy4_right = { 
                                             /* 1 Life: WHITE images*/          new List<Image> { Properties.Resources.enemy4_right_1, Properties.Resources.enemy4_right_2 }, 
                                             
                                             /* 2 Life: GREEN + YELLOW images*/ new List<Image> { Properties.Resources.enemy4_green_right_1, Properties.Resources.enemy4_green_right_2, Properties.Resources.enemy4_yellow_right_1,Properties.Resources.enemy4_yellow_right_2  },
                                             
                                             /* 3 Life: WHITE + YELLOW images*/ new List<Image> { Properties.Resources.enemy4_down_1, Properties.Resources.enemy4_down_2, Properties.Resources.enemy4_yellow_right_1,Properties.Resources.enemy4_yellow_right_2, }, 
                                             
                                             /* 4 Life: WHITE + GREEN images*/  new List<Image> { Properties.Resources.enemy4_right_1, Properties.Resources.enemy4_right_2, Properties.Resources.enemy4_green_right_1, Properties.Resources.enemy4_green_right_2 } 
                                            };

        public static List<Image> enemy4_img_down = new List<Image> { Properties.Resources.enemy4_down_1, Properties.Resources.enemy4_down_2 };

        public static List<Image> enemy4_img_up = new List<Image> { Properties.Resources.enemy4_up_1, Properties.Resources.enemy4_up_2 };

        public static List<Image> enemy4_img_left = new List<Image> { Properties.Resources.enemy4_left_1, Properties.Resources.enemy4_left_2 };

        public static List<Image> enemy4_img_right = new List<Image> { Properties.Resources.enemy4_right_1, Properties.Resources.enemy4_right_2 };

        public static List<Image> enemy4_img_down_bonus = new List<Image> { Properties.Resources.enemy4_bonus_down_1, Properties.Resources.enemy4_bonus_down_2, Properties.Resources.enemy4_down_1, Properties.Resources.enemy4_down_2 };

        public static List<Image> enemy4_img_up_bonus = new List<Image> { Properties.Resources.enemy4_bonus_up_1, Properties.Resources.enemy4_bonus_up_2, Properties.Resources.enemy4_up_1, Properties.Resources.enemy4_up_2 };

        public static List<Image> enemy4_img_left_bonus = new List<Image> { Properties.Resources.enemy4_bonus_left_1, Properties.Resources.enemy4_bonus_left_2, Properties.Resources.enemy4_left_1, Properties.Resources.enemy4_left_2 };

        public static List<Image> enemy4_img_right_bonus = new List<Image> { Properties.Resources.enemy4_bonus_right_1, Properties.Resources.enemy4_bonus_right_2, Properties.Resources.enemy4_right_1, Properties.Resources.enemy4_right_2 };
        

    }
}
