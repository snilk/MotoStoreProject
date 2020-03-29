// -----------------------------------------------------------------------
//  <copyright>
//    Copyright © 2014 MTS Systems Corporation.  All rights reserved.
//    This software is furnished under a license and may be used and
//    copied only in accordance with the terms of such license
//    and with inclusion of the above copyright notice.
//    <author>
//      MTS Systems Corporation, AlexanderEmelyanenko, 03/29/2020 3:42 PM
//    </author>
//  </copyright>
// -----------------------------------------------------------------------

namespace MotoStore.Domain.EF
{
    public class MotoImages
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public string ImageUrl { get; set; }

        public virtual Motorcycle Motorcycle { get; set; }
    }
}