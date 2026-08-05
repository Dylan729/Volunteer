using System;
using System.Collections.Generic;
using System.Text;

namespace VolunteerConnect.Models;

public class OpportunityRegistration
{
    public int OpportunityId { get; set; }
    public string PreferredName { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string Availability { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public DateTime RegisteredOn { get; set; }
}
