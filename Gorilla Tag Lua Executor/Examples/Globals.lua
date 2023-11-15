-- You WILL need DnSpy for this or prior knowledge with modding Gorilla Tag

-- _GorillaBattleManager    => GorillaBattleManager.instance
-- _GorillaPlayer           => GorillaLocomotion.Player.instance
-- _GorillaDayNight         => GorillaDayNight.instance
-- _GorillaComputer         => GorillaParent.instance
-- _BetterDayNightManager   => BetterDayNightManager.instance
-- _GorillaParent           => GorillaParent.instance
-- _PhotonNetworkController => PhotonNetworkController.Instance
-- InputManager             => Custom Input Manager

-- Example Usage
print(_GorillaPlayer.jumpMultiplier)

-- You could also do (This will most likely get you banned)
_GorillaPlayer.jumpMultiplier = 1000;

-- Now it's your turn to find interesting stuff inside these classes with DnSpy to see if you can create something cool :)

-- There's also _G. It holds stuff like Author of script and other stuff
-- Exmaple Usage
_G.Author = "Vidde"
print(_G.Author)