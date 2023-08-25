
namespace Keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepository;

  public KeepsService(KeepsRepository keepsRepository)
  {
    _keepsRepository = keepsRepository;
  }

  internal Keep createKeep(Keep keepData)
  {
    int keepId = _keepsRepository.createKeep(keepData);
    Keep keep = getKeepById(keepId);
    return keep;
  }

  internal Keep editKeep(Keep keepData, int keepId)
  {
    Keep OriginalKeep = getKeepById(keepId);
    if (keepData.CreatorId != OriginalKeep.CreatorId)
    {
      throw new Exception($"ERROR Bad KeepId: {keepData.Id}");
    }
    OriginalKeep.Name = keepData.Name ?? OriginalKeep.Name;
    OriginalKeep.Description = keepData.Description ?? OriginalKeep.Description;
    OriginalKeep.Img = keepData.Img ?? OriginalKeep.Img;

    _keepsRepository.editKeep(OriginalKeep);
    return OriginalKeep;
  }

  internal Keep getKeepById(int keepId)
  {
    Keep keep = _keepsRepository.getKeepById(keepId);
    if (keep == null)
    {
      throw new Exception($"Bad Keep iD: {keepId}");
    }
    return keep;
  }

  internal Keep getKeepByIdAndIncreaseViews(int keepId, string userId = null)
  {
    Keep keep = getKeepById(keepId);
    keep.Views++;
    _keepsRepository.editKeep(keep);
    return keep;
  }

  internal List<Keep> getKeeps()
  {
    List<Keep> keeps = _keepsRepository.getKeeps();
    return keeps;
  }

  internal void removeKeep(int keepId, string userId)
  {
    Keep originalKeep = getKeepById(keepId);
    if (originalKeep.CreatorId != userId)
    {
      throw new Exception($"Bad Keep Id: {keepId}");
    }
    _keepsRepository.removeKeep(keepId);
    return;
  }
}
