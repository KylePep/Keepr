
namespace Keepr.Models;

public class Vault : RepoItem<int>
{
  public string Description { get; set; }
  public string Name { get; set; }
  public string Img { get; set; }
  public string CreatorId { get; set; }
  public bool? isPrivate { get; set; }
  public Profile Creator { get; set; }
}
