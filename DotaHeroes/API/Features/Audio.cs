using CommandSystem.Commands.RemoteAdmin;
using Exiled.API.Features;
using MEC;
using Mirror;
using PlayerRoles;
using SCPSLAudioApi.AudioCore;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes.API.Features
{
    public static class Audio
    {
        public const string SoundBotName = "Sound";

        private static NoclipCommand noclipCommand = new NoclipCommand();

        public static Player Play(Vector3 position, string filepath, float volume = 100, bool isLoop = false, Player owner = null)
        {
            if (!Plugin.Instance.Config.IsUsingSounds) return default;

            var npc = Npc.Spawn(SoundBotName + AudioPlayerBase.AudioPlayers.Count, RoleTypeId.Tutorial, AudioPlayerBase.AudioPlayers.Count + Player.List.Count, string.Empty, position);
            npc.IsGodModeEnabled = true;
            noclipCommand.Execute(Features.Utils.EmptyArraySegment, npc.Sender, out string _);
            npc.Scale = Vector3.zero;

            var audioPlayer = AudioPlayerBase.Get(npc.ReferenceHub);
            audioPlayer.Loop = isLoop;
            audioPlayer.Enqueue(filepath, 0);
            audioPlayer.Play(0);

            audioPlayer.Volume = volume;

            Timing.RunCoroutine(WaitForAudioEndCoroutine(audioPlayer, npc, owner));

            return npc;
        }

        public static void Stop(Player sound)
        {
            if (!Plugin.Instance.Config.IsUsingSounds) return;

            if (!sound.Nickname.Contains(SoundBotName)) return;

            AudioPlayerBase.AudioPlayers.Remove(sound.ReferenceHub);
            NetworkServer.Destroy(sound.GameObject); //exiled team, disconnect is dont work lol
        }

        public static void StopLoop(Player sound)
        {
            if (!Plugin.Instance.Config.IsUsingSounds) return;

            if (!sound.Nickname.Contains(SoundBotName)) return;

            sound.IsNPC = false; //:))
        }

        private static IEnumerator<float> WaitForAudioEndCoroutine(AudioPlayerBase audioPlayer, Player player, Player owner = null)
        {
            if (audioPlayer.Loop)
            {
                while (player.IsNPC) //:)), this shit im know
                {
                    if (owner != null)
                    {
                        player.Teleport(owner.Position);
                    }

                    yield return Timing.WaitForOneFrame;
                }
            }
            else
            {
                yield return Timing.WaitForSeconds((float)audioPlayer.VorbisReader.TotalTime.Seconds + 3);
            }

            Stop(player);
        }
    }
}
